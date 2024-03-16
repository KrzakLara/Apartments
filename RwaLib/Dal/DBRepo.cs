using Microsoft.ApplicationBlocks.Data;
using RwaLib.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace RwaLib.Dal
{

    public class DBRepo : IRepo
    {
        private static string CS = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        public IEnumerable<object> ApartmentPictures => throw new NotImplementedException();

        public IList<Tag> LoadTags()
        {
            IList<Tag> tags = new List<Tag>();

            var tblTags = SqlHelper.ExecuteDataset(CS, nameof(LoadTags)).Tables[0];
            foreach (DataRow row in tblTags.Rows)
            {
                tags.Add(
                    new Tag
                    {
                        Id = (int)row[nameof(Tag.Id)],
                        Name = row[nameof(Tag.Name)].ToString(),
                        TaggedApartments = GetTaggedApartments((int)row[nameof(Tag.Id)])
                    }
                );
            }
            return tags;
        }

        public IList<ApartmentOwner> GetOwners()
        {
            IList<ApartmentOwner> owners = new List<ApartmentOwner>();
            var tblOwners = SqlHelper.ExecuteDataset(CS, nameof(GetOwners)).Tables[0];
            
            foreach (DataRow row in tblOwners.Rows)
            {
                owners.Add(
                    new ApartmentOwner
                    {
                        Id = (int)row[nameof(Tag.Id)],
                        Name = row[nameof(Tag.Name)].ToString()                        
                    }
                );
            }
            return owners;
        }

        public int GetTaggedApartments(int tagId)
        {
            var tblTaggedApartments = SqlHelper.ExecuteDataset(CS, nameof(GetTaggedApartments), tagId).Tables[0];
            if (tblTaggedApartments.Rows.Count == 0) return 0;
            DataRow row = tblTaggedApartments.Rows[0];
            return (int)row[nameof(Tag.TaggedApartments)];
        }

        public IList<Apartment> GetApartmentsByStatusId(int statusId)
        {
            IList<Apartment> apartments = new List<Apartment>();
            var tblApartments = SqlHelper.ExecuteDataset(CS, nameof(GetApartmentsByStatusId),statusId).Tables[0];

            foreach (DataRow row in tblApartments.Rows)
            {
                apartments.Add(
                    new Apartment
                    {
                        Id = (int)row[nameof(Apartment.Id)],
                        Name = row[nameof(Apartment.Name)].ToString(),
                        Price = Convert.ToDecimal(row[nameof(Apartment.Price)]),
                        Address = row[nameof(Apartment.Address)].ToString(),
                        MaxChildren = (int)row[nameof(Apartment.MaxChildren)],
                        MaxAdults = (int)row[nameof(Apartment.MaxAdults)],
                        TotalRooms = (int)row[nameof(Apartment.TotalRooms)],
                        CityId = (int)row[nameof(Apartment.CityId)],
                        NameCity = GetCityName((int)row[nameof(Apartment.CityId)]),
                        BeachDistance = (int)row[nameof(Apartment.BeachDistance)],
                        StatusId = (int)row[nameof(Apartment.StatusId)],
                        StatusName = GetStatusName((int)row[nameof(Apartment.StatusId)]),
                        OwnerId = (int)row[nameof(Apartment.OwnerId)]

                    }
                );
            }
            return apartments;
        }

        public IList<Tag> LoadApartmentTags(int apartmentId)
        {
            IList<Tag> tags = new List<Tag>();

            var tblTags = SqlHelper.ExecuteDataset(CS, nameof(LoadApartmentTags),apartmentId).Tables[0];

            foreach (DataRow row in tblTags.Rows)
            {
                tags.Add(
                    new Tag
                    {
                        Id = (int)row[nameof(Tag.Id)],
                        Name = row[nameof(Tag.Name)].ToString(),
                        Type = row[nameof(Tag.Type)].ToString()
                    }
                );
            }
            return tags;
        }
               


        public Apartment GetApartmentById(int apartmentId)
        {
            Apartment apartment = new Apartment();

            var tblApartment = SqlHelper.ExecuteDataset(CS, nameof(GetApartmentById), apartmentId).Tables[0];

            if (tblApartment.Rows.Count == 0) return null;

            DataRow row = tblApartment.Rows[0];
            apartment.Id = (int)row[nameof(Apartment.Id)];
            apartment.Name = row[nameof(Apartment.Name)].ToString();
            apartment.OwnerId = (int)row[nameof(Apartment.OwnerId)];
            apartment.OwnerName = GetOwnerName((int)row[nameof(Apartment.OwnerId)]);
            apartment.StatusId = (int)row[nameof(Apartment.StatusId)];
            apartment.Price = (decimal)row[nameof(Apartment.Price)];            
            apartment.MaxAdults = (int)row[nameof(Apartment.MaxAdults)];
            apartment.MaxChildren = (int)row[nameof(Apartment.MaxChildren)];
            apartment.TotalRooms = (int)row[nameof(Apartment.TotalRooms)];
            apartment.Address = row[nameof(Apartment.Address)].ToString();
            apartment.BeachDistance = (int)row[nameof(Apartment.BeachDistance)];
            apartment.CityId = (int)row[nameof(Apartment.CityId)];

            return apartment;
        }

        public int GetCity(int? cityId)
        {
            var tblCity = SqlHelper.ExecuteDataset(CS, nameof(GetApartmentById), cityId).Tables[0];
            if (tblCity.Rows.Count == 0) return 0;
            DataRow row = tblCity.Rows[0];
            return (int)row[nameof(Apartment.CityId)];
        }

        public string GetOwnerName(int ownerId)
        {            
            var tblApartmentOwner = SqlHelper.ExecuteDataset(CS, nameof(GetOwnerName), ownerId).Tables[0];
            if (tblApartmentOwner.Rows.Count == 0) return null;

            DataRow row = tblApartmentOwner.Rows[0];

            return row[nameof(ApartmentOwner.Name)].ToString(); 
        }


        public int GetApartmentStatus(int apartmentId)
        {            
            var tblApartmentStatus = SqlHelper.ExecuteDataset(CS, nameof(GetApartmentStatus), apartmentId).Tables[0];
            if (tblApartmentStatus.Rows.Count == 0) return 0;
            DataRow row = tblApartmentStatus.Rows[0];
            return (int)row[nameof(Apartment.StatusId)];
        }

        public IList<string> LoadTagType()
        {
            IList<String> type = new List<String>();

            var tblTypeTags = SqlHelper.ExecuteDataset(CS, nameof(LoadTagType)).Tables[0];
            if (tblTypeTags == null) return null;

            foreach (DataRow row in tblTypeTags.Rows)
            {
                type.Add(row[nameof(Tag.Type)].ToString());
            }
            return type;
        }

        public void EditApartment(Apartment apartment)
        {
            SqlHelper.ExecuteNonQuery(CS, nameof(EditApartment), apartment.Id, apartment.Name, apartment.Price, apartment.MaxAdults, apartment.MaxChildren, apartment.BeachDistance, apartment.StatusId, apartment.TotalRooms);
        }

        public IList<Apartment> LoadApartmentsByTagID(int tagID)
        {
            IList<Apartment> apartments = new List<Apartment>();

            var tblApartments = SqlHelper.ExecuteDataset(CS, nameof(LoadApartmentsByTagID), tagID).Tables[0];
            foreach (DataRow row in tblApartments.Rows)
            {
                apartments.Add(
                    new Apartment
                    {
                        Name = row[nameof(Tag.Name)].ToString()
                    }
                );
            }

            return apartments;
        }



        public User AuthUser(string username, string password)
        {
            var tblAuth = SqlHelper.ExecuteDataset(CS, nameof(AuthUser), username, password).Tables[0];
            if (tblAuth.Rows.Count == 0) return null;

            DataRow row = tblAuth.Rows[0];
            return new User
            {
                Id = (int)row[nameof(User.Id)],
                UserName = row[nameof(User.UserName)].ToString(),
                PhoneNumber = row[nameof(User.PhoneNumber)].ToString(),
                Address = row[nameof(User.Address)].ToString(),
                Email = row[nameof(User.Email)].ToString(),
                CreatedAt = DateTime.Parse(row[nameof(User.CreatedAt)].ToString()),
                PasswordHash = row[nameof(User.PasswordHash)].ToString()
            };
        }

        public IList<City> LoadCities()
        {
            IList<City> cities = new List<City>();

            var tblCity = SqlHelper.ExecuteDataset(CS, nameof(LoadCities)).Tables[0];
            foreach (DataRow row in tblCity.Rows)
            {
                cities.Add(
                    new City
                    {
                        Id = (int)row[nameof(City.Id)],
                        Name = row[nameof(City.Name)].ToString(),
                    }
                );
            }

            return cities;
        }

        public IList<User> LoadUsers()
        {
            IList<User> users = new List<User>();

            var tblUsers = SqlHelper.ExecuteDataset(CS, nameof(LoadUsers)).Tables[0];
            foreach (DataRow row in tblUsers.Rows)
            {
                users.Add(
                    new User
                    {
                        Id = (int)row[nameof(User.Id)],
                        UserName = row[nameof(User.UserName)].ToString(),
                        PhoneNumber = row[nameof(User.PhoneNumber)].ToString(),
                        Address = row[nameof(User.Address)].ToString(),
                        Email = row[nameof(User.Email)].ToString(),
                        CreatedAt = DateTime.Parse(row[nameof(User.CreatedAt)].ToString())

                    }
                );
            }

            return users;
        }

        public IList<Status> LoadStatus()
        {
            IList<Status> status = new List<Status>();

            var tblStatus = SqlHelper.ExecuteDataset(CS, nameof(LoadStatus)).Tables[0];

            new Models.Status { Id = 0, Name = "(odabir statusa)" };
            new Models.Status { Id = 1, Name = "Zauzeto" };
            new Models.Status { Id = 2, Name = "Rezervirano" };
            new Models.Status { Id = 3, Name = "Slobodno" };


            return status;

        }


        public IList<Order> LoadOrder()
        {
            IList<Order> order = new List<Order>();

            var tblOrder = SqlHelper.ExecuteDataset(CS, nameof(LoadOrder)).Tables[0];

            new Models.Order { Id = 0, Name = "(kriterij sortiranja)" };
            new Models.Order { Id = 1, Name = "BrojSoba" };
            new Models.Order { Id = 2, Name = "BrojOdraslih" };
            new Models.Order { Id = 3, Name = "BrojDjece" };
            new Models.Order { Id = 4, Name = "Cijena" };


            return order;

        }

        public IList<ApartmentOwner> LoadOwners()
        {
            var ds = SqlHelper.ExecuteDataset(CS, nameof(LoadOwners)).Tables[0];

            var ownerList = new List<RwaLib.Models.ApartmentOwner>();

            ownerList.Add(new RwaLib.Models.ApartmentOwner { Id = 0, Name = "(odabir vlasnika)" });

            foreach (DataRow row in ds.Rows)
            {
                var owner = new RwaLib.Models.ApartmentOwner();
                owner.Id = Convert.ToInt32(row["ID"]);
                owner.Name = row["Name"].ToString();
                ownerList.Add(owner);
            }

            return ownerList;

        }
        public IList<Apartment> LoadApartments()
        {
            IList<Apartment> apartments = new List<Apartment>();

            var tblApartments = SqlHelper.ExecuteDataset(CS, nameof(LoadApartments)).Tables[0];
            foreach (DataRow row in tblApartments.Rows)
            {
                apartments.Add(
                    new Apartment
                    {
                        Id = (int)row[nameof(Apartment.Id)],
                        Name = row[nameof(Apartment.Name)].ToString(),
                        Price = Convert.ToDecimal(row[nameof(Apartment.Price)]),
                        Address = row[nameof(Apartment.Address)].ToString(),
                        MaxChildren = (int)row[nameof(Apartment.MaxChildren)],
                        MaxAdults= (int)row[nameof(Apartment.MaxAdults)],
                        TotalRooms = (int)row[nameof(Apartment.TotalRooms)],
                        CityId = (int)row[nameof(Apartment.CityId)],
                        NameCity = GetCityName((int)row[nameof(Apartment.CityId)]),
                        BeachDistance = (int)row[nameof(Apartment.BeachDistance)],
                        StatusId = (int)row[nameof(Apartment.StatusId)],
                        StatusName = GetStatusName((int)row[nameof(Apartment.StatusId)]),
                        OwnerId = (int)row[nameof(Apartment.OwnerId)]
                    }
                ); 
            }

            return apartments;
        }

        public string GetStatusName(int statusId)
        {
            if (statusId == 1)
                return "Zauzeto";
            else if (statusId == 2)
                return "Rezervirano";
            else 
                return "Slobodno";
        }

        private string GetCityName(int id)
        {
            City grad = new City();
                 
            var tblCity = SqlHelper.ExecuteDataset(CS, nameof(GetCityName), id).Tables[0];

            if (tblCity == null) return null;

            DataRow row = tblCity.Rows[0];

            return grad.Name = row[nameof(City.Name)].ToString();
        }

        public IList<Apartment> GetApartments()
        {
            var ds = SqlHelper.ExecuteDataset(CS, nameof(GetApartments)).Tables[0];

            var apList = new List<Models.Apartment>();
            foreach (DataRow row in ds.Rows)
            {
                var ap = new Models.Apartment();
                ap.Id = Convert.ToInt32(row["ID"]);
                ap.Guid = Guid.Parse(row["Guid"].ToString());
                ap.CreatedAt = Convert.ToDateTime(row["CreatedAt"]);
                ap.DeletedAt = row["DeletedAt"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["DeletedAt"]) : null;
                ap.OwnerId = Convert.ToInt32(row["OwnerId"]);
                ap.TypeId = Convert.ToInt32(row["TypeId"]);
                ap.StatusId = Convert.ToInt32(row["StatusId"]);
                ap.CityId = row["CityId"] != DBNull.Value ? (int?)Convert.ToInt32(row["CityId"]) : null;
                ap.Address = row["Address"].ToString();
                ap.Name = row["Name"].ToString();
                ap.Price = Convert.ToDecimal(row["Price"]);
                ap.MaxAdults = row["MaxAdults"] != DBNull.Value ? (int?)Convert.ToInt32(row["MaxAdults"]) : null;
                ap.MaxChildren = row["MaxChildren"] != DBNull.Value ? (int?)Convert.ToInt32(row["MaxChildren"]) : null;
                ap.TotalRooms = row["TotalRooms"] != DBNull.Value ? (int?)Convert.ToInt32(row["TotalRooms"]) : null;
                ap.BeachDistance = row["BeachDistance"] != DBNull.Value ? (int?)Convert.ToInt32(row["BeachDistance"]) : null;
                apList.Add(ap);
            }

            return apList;
        }

        public List<Tag> GetApartmentTags(int apartmentId)
        {
            var commandParameters = new List<SqlParameter>(); commandParameters.Add(new SqlParameter("@apartmentId", apartmentId));
            var ds = SqlHelper.ExecuteDataset(CS, nameof(GetApartmentTags)).Tables[0];
            var tags = new List<Tag>();
            foreach (DataRow row in ds.Rows)
            {
                tags.Add(new Models.Tag
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Name = row["Name"].ToString(),
                });
            }
            return tags;
        }

        public List<ApartmentPicture> GetApartmentPictures(int apartmentId)
        {
            var commandParameters = new List<SqlParameter>();
            commandParameters.Add(new SqlParameter("@apartmentId", apartmentId));
            var ds = SqlHelper.ExecuteDataset(CS, nameof(GetApartmentPictures)).Tables[0];

            var pics = new List<ApartmentPicture>();
            foreach (DataRow row in ds.Rows)
            {
                pics.Add(new Models.ApartmentPicture
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Path = row["Path"].ToString(),
                    Name = row["Name"].ToString(),
                    IsRepresentative = bool.Parse(row["IsRepresentative"].ToString())
                });
            }
            return pics;
        }
        public List<City> GetCities()
        {
            var ds = SqlHelper.ExecuteDataset(CS, nameof(GetCities)).Tables[0];
            var cityList = new List<City>();
            cityList.Add(new City { Id = 0, Name = "(odabir grada)" });
            foreach (DataRow row in ds.Rows)
            {
                var city = new City();
                city.Id = Convert.ToInt32(row["ID"]);
                city.Guid = Guid.Parse(row["Guid"].ToString());
                city.Name = row["Name"].ToString();
                cityList.Add(city);
            }
            return cityList;
        }

        public List<Tag> GetTags()
        {
            var ds = SqlHelper.ExecuteDataset(CS, nameof(GetTags)).Tables[0];
            var tagList = new List<Tag>();
            tagList.Add(new Tag { Id = 0, Name = "(odabir taga)" });
            foreach (DataRow row in ds.Rows)
            {
                var tag = new Tag();
                tag.Id = Convert.ToInt32(row["ID"]);
                tag.Name = row["Name"].ToString();
                tagList.Add(tag);
            }
            return tagList;
        }

        public List<ApartmentOwner> GetApartmentOwners()
        {
            var ds = SqlHelper.ExecuteDataset(CS, nameof(GetApartmentOwners)).Tables[0];
            var ownerList = new List<ApartmentOwner>();
            ownerList.Add(new ApartmentOwner { Id = 0, Name = "(odabir vlasnika)" });
            foreach (DataRow row in ds.Rows)
            {
                var owner = new ApartmentOwner();
                owner.Id = Convert.ToInt32(row["ID"]);
                owner.Name = row["Name"].ToString();
                ownerList.Add(owner);
            }
            return ownerList;
        }
        public IList<Status> GetStatuses()
        {
            return new List<Status>
            {
                new Status { Id = 0, Name = "(odabir statusa)" },
                new Status { Id = 1, Name = "Zauzeto" },
                new Status { Id = 2, Name = "Rezervirano" },
                new Status { Id = 3, Name = "Slobodno" }
            };
        }

        public List<Models.Order> GetOrders()
        {
            return new List<Models.Order>
           {
            new Models.Order { Name = "BrojSoba" },
            new Models.Order {Name = "BrojOdraslih" },
            new Models.Order {  Name = "BrojDjece" },
            new Models.Order { Name = "Cijena" },
             };
        }

        public void CreateApartment(Apartment apartment)
        {
            SqlHelper.ExecuteNonQuery(CS, nameof(CreateApartment), apartment.NameEng, apartment.Price, apartment.MaxAdults, apartment.MaxChildren, apartment.TotalRooms, apartment.BeachDistance, apartment.CityId, apartment.OwnerId, apartment.Address, apartment.StatusId);
        }

        public void UpdateApartment(Apartment apartment)
        {
            var commandParameters = new List<SqlParameter>();
            commandParameters.Add(new SqlParameter("@id", apartment.Id));
            commandParameters.Add(new SqlParameter("@guid", apartment.Guid));
            commandParameters.Add(new SqlParameter("@ownerId", apartment.OwnerId));
            commandParameters.Add(new SqlParameter("@typeId", apartment.TypeId));
            commandParameters.Add(new SqlParameter("@statusId", apartment.StatusId));
            commandParameters.Add(new SqlParameter("@cityId", apartment.CityId));
            commandParameters.Add(new SqlParameter("@address", apartment.Address));
            commandParameters.Add(new SqlParameter("@name", apartment.Name));
            commandParameters.Add(new SqlParameter("@price", apartment.Price));
            commandParameters.Add(new SqlParameter("@maxAdults", apartment.MaxAdults));
            commandParameters.Add(new SqlParameter("@maxChildren", apartment.MaxChildren));
            commandParameters.Add(new SqlParameter("@totalRooms", apartment.TotalRooms));
            commandParameters.Add(new SqlParameter("@beachDistance", apartment.BeachDistance));
            DataTable dtTags = new DataTable();
            dtTags.Columns.AddRange(
            new DataColumn[1] {
            new DataColumn("Key", typeof(int)) });

            commandParameters.Add(new SqlParameter("@tags", dtTags));
            SqlHelper.ExecuteNonQuery(CS, CommandType.StoredProcedure, "dbo.UpdateApartment", commandParameters.ToArray());
        }

        public void DeleteApartment(int apartmanId)
        {
            SqlHelper.ExecuteNonQuery(CS,nameof(DeleteApartment), apartmanId);
        }

        public void AddTag(int tagId, string name)
        {
            SqlHelper.ExecuteNonQuery(CS, nameof(AddTag), tagId, name);
        }

        public void DeleteTag(int tagID)
        {
            SqlHelper.ExecuteNonQuery(CS, nameof(DeleteTag), tagID);
        }

        public void AddApartment(Apartment apartment)
        {
            IList<Apartment> apartments = new List<Apartment>();

            var tblApartments = SqlHelper.ExecuteDataset(CS, nameof(AddApartment),apartment.OwnerName,apartment.StatusId,apartment.CityName, apartment.Address ,apartment.Name,apartment.Price,apartment.MaxAdults,apartment.MaxChildren,apartment.TotalRooms,apartment.BeachDistance).Tables[0];
            foreach (DataRow row in tblApartments.Rows)
            {
                apartments.Add(
                    new Apartment
                    {
                        Id = (int)row[nameof(Apartment.Id)],
                        CityName = new City((int)row[nameof(Apartment.CityId)], row[nameof(Apartment.CityName)].ToString()),
                        Address = row[nameof(Apartment.Address)].ToString(),
                        Name = row[nameof(Apartment.Name)].ToString(),
                        OwnerName = row[nameof(Apartment.OwnerName)].ToString(),
                        StatusName = row[nameof(Apartment.StatusName)].ToString(),
                        OwnerId = (int)row[nameof(Apartment.OwnerId)],
                        Price = (int)row[nameof(Apartment.Price)],
                        MaxAdults = (int)row[nameof(Apartment.MaxAdults)],
                        MaxChildren = (int)row[nameof(Apartment.MaxChildren)],
                        TotalRooms = (int)row[nameof(Apartment.TotalRooms)],
                        BeachDistance = (int)row[nameof(Apartment.BeachDistance)]

                    }
                );
            }

        }

        public void DeleteApartmentTag(int tagId, int apartmentId)
        {
            SqlHelper.ExecuteNonQuery(CS, nameof(DeleteApartmentTag), tagId, apartmentId);
        }

        public void CreateNewUser(User user)
        {
            SqlHelper.ExecuteNonQuery(CS, nameof(CreateNewUser), user.Email, user.UserName, Cryptography.HashPassword(user.PasswordHash),user.Address,user.PhoneNumber);
        }

        public object GetApartmentOwnerById(int ownerId)
        {
            ApartmentOwner apartmentOwner = new ApartmentOwner();

            var tblApartmentOwner = SqlHelper.ExecuteDataset(CS, nameof(GetApartmentOwnerById), ownerId).Tables[0];
            if (tblApartmentOwner.Rows.Count == 0) return null;

            DataRow row = tblApartmentOwner.Rows[0];

            apartmentOwner.Id = (int)row[nameof(ApartmentOwner.Id)];
            apartmentOwner.Name = row[nameof(ApartmentOwner.Name)].ToString();

            return apartmentOwner;
        }

    

        public void CreateNewReview(Review review)
        {
            SqlHelper.ExecuteNonQuery(CS, nameof(CreateNewReview), review.UserId, review.Stars, review.Description, review.ApartmentId);
        }

       


        public void CreateNewReservation(Reservation reservation)
        {
            SqlHelper.ExecuteNonQuery(CS, nameof(CreateNewReservation), reservation.ApartmentId, reservation.FirstName + " " + reservation.LastName, reservation.Email, reservation.Children, reservation.Phone, reservation.Adults);

        }

        public Apartment GetApartmentByID(int apartmentId)
        {
            Apartment apartment = new Apartment();

            var tblApartment = SqlHelper.ExecuteDataset(CS, nameof(GetApartmentByID), apartmentId).Tables[0];
            if (tblApartment.Rows.Count == 0) return null;

            DataRow row = tblApartment.Rows[0];

            apartment.Id = (int)row[nameof(Apartment.Id)];
            apartment.NameEng = row[nameof(Apartment.NameEng)].ToString();
            apartment.MaxAdults = (int)row[nameof(Apartment.MaxAdults)];
            apartment.MaxChildren = (int)row[nameof(Apartment.MaxChildren)];
            apartment.TotalRooms = (int)row[nameof(Apartment.TotalRooms)];
            apartment.Price = (int)row[nameof(Apartment.Price)];
            apartment.City = new City((int)row[nameof(Apartment.CityId)], row[nameof(Apartment.CityName)].ToString());
            apartment.StatusId = (int)row[nameof(Apartment.StatusId)];
            apartment.StatusName = row[nameof(Apartment.StatusName)].ToString();
            apartment.BeachDistance = (int)row[nameof(Apartment.BeachDistance)];
            apartment.Address = row[nameof(Apartment.Address)].ToString();
            apartment.OwnerId = (int)row[nameof(Apartment.OwnerId)];
            apartment.Review = (int)GetReview(apartment.Id);

            return apartment;
        }

        private object GetReview(int id)
        {
            IList<int> numberOfReviews = new List<int>();

            var tblnumberOfStars = SqlHelper.ExecuteDataset(CS, nameof(GetReview), id).Tables[0];
            if (tblnumberOfStars.Rows.Count == 0) return 0;

            foreach (DataRow row in tblnumberOfStars.Rows)
            {
                numberOfReviews.Add((int)row["Stars"]);
            }

            int suma = 0;

            foreach (int stars in numberOfReviews)
            {
                suma += stars;
            }

            return suma / numberOfReviews.Count;
        }

        public IList<ApartmentPicture> GetApartmentPictures()
        {
            IList<ApartmentPicture> apartmentPictures = new List<ApartmentPicture>();

            var tblApartmentsPictures = SqlHelper.ExecuteDataset(CS, nameof(GetApartmentPictures)).Tables[0];
            if (tblApartmentsPictures == null) return null;

            foreach (DataRow row in tblApartmentsPictures.Rows)
            {
                ApartmentPicture picture = new ApartmentPicture();
                picture.Id = (int)row[nameof(ApartmentPicture.Id)];
                picture.ApartmentId = (int)row[nameof(ApartmentPicture.ApartmentId)];
                picture.Name = row[nameof(ApartmentPicture.Name)].ToString();
                picture.IsRepresentative = row[nameof(ApartmentPicture.IsRepresentative)].ToString() == "0" ? false : true;
                picture.Path = (string)row[nameof(ApartmentPicture.Path)];

                apartmentPictures.Add(picture);
            }

            return apartmentPictures;
        }

        public int GetNumberOfApartmentPictures(int apartmentId)
        {

            var tblNumberOfPictures = SqlHelper.ExecuteDataset(CS, nameof(GetNumberOfApartmentPictures), apartmentId).Tables[0];
            if (tblNumberOfPictures.Rows.Count == 0) return 0;

            DataRow row = tblNumberOfPictures.Rows[0];

            int broj = (int)row[nameof(Apartment.NumberOfPictures)];

            return broj;
        }

        public void SetRepresentativePicture(int apartmentId, int newRepresentative)
        {
            SqlHelper.ExecuteNonQuery(CS, nameof(SetRepresentativePicture), apartmentId, newRepresentative);
        }

        public void DeleteApartmentPictureByID(int imageid)
        {
            SqlHelper.ExecuteDataset(CS, nameof(DeleteApartmentPictureByID), imageid);
        }

        public string GetImagePath(int imageId)
        {
            var tblPath = SqlHelper.ExecuteDataset(CS, nameof(GetImagePath), imageId).Tables[0];
            if (tblPath.Rows.Count == 0) return null;

            DataRow row = tblPath.Rows[0];

            return row[nameof(ApartmentPicture.Path)].ToString();
        }

        public IList<Tag> LoadApartmentsByTagID()
        {
            throw new NotImplementedException();
        }

        public IList<User> AddUser()
        {
            throw new NotImplementedException();
        }

        public IList<Apartment> AddApartment()
        {
            throw new NotImplementedException();
        }

        public IList<User> AuthUser()
        {
            throw new NotImplementedException();
        }

        public IList<Tag> AddTag()
        {
            throw new NotImplementedException();
        }

        public IList<Tag> GetApartmentTags()
        {
            throw new NotImplementedException();
        }

      

        IList<City> IRepo.GetCities()
        {
            throw new NotImplementedException();
        }

        IList<Apartment> IRepo.GetApartmentOwners()
        {
            throw new NotImplementedException();
        }

        public IList<Apartment> CreateApartment()
        {
            throw new NotImplementedException();
        }

        public IList<Apartment> UpdateApartment()
        {
            throw new NotImplementedException();
        }


        public IList<Apartment> DeleteApartment()
        {
            throw new NotImplementedException();
        }

        public IList<Apartment> GetAllApartments()
        {
            IList<Apartment> apartments = new List<Apartment>();

            var tblApartments = SqlHelper.ExecuteDataset(CS, nameof(GetAllApartments)).Tables[0];
            if (tblApartments.Rows.Count == 0) return null;

            for (int i = 0; i < tblApartments.Rows.Count; i++)
            {
                DataRow row = tblApartments.Rows[i];
                apartments.Add(new Apartment
                {
                    Id = (int)row[nameof(Apartment.Id)],
                    Name = row[nameof(Apartment.NameEng)].ToString(),
                    NameEng = row[nameof(Apartment.NameEng)].ToString(),
                    MaxAdults = (int)row[nameof(Apartment.MaxAdults)],
                    MaxChildren = (int)row[nameof(Apartment.MaxChildren)],
                    TotalRooms = (int)row[nameof(Apartment.TotalRooms)],
                    Price = (int)row[nameof(Apartment.Price)],
                    StatusId = (int)row[nameof(Apartment.StatusId)],
                    BeachDistance = (int)row[nameof(Apartment.BeachDistance)],
                    CityId = ((int)row[nameof(Apartment.CityId)]),
                    StatusName = GetStatusName((int)row[nameof(Apartment.StatusId)]),
                    NumberOfPictures = GetNumberOfApartmentPictures((int)row[nameof(Apartment.Id)]),
                    Review = (int)GetReview((int)row[nameof(Apartment.Id)])
                });
            }

            return apartments;
        }

        public IList<Apartment> GetAllApartmentsByStatusId(int? statusId)
        {
            IList<Apartment> apartments = new List<Apartment>();
            DataTable tblApartments;
            if (statusId.HasValue && statusId.Value != 0)
            {
                tblApartments = SqlHelper.ExecuteDataset(CS, nameof(GetAllApartmentsByStatusId), statusId).Tables[0];

                if (tblApartments == null) return null;

                for (int i = 0; i < tblApartments.Rows.Count; i++)
                {
                    DataRow row = tblApartments.Rows[i];
                    apartments.Add(new Apartment
                    {
                        Id = (int)row[nameof(Apartment.Id)],
                        NameEng = row[nameof(Apartment.NameEng)].ToString(),
                        MaxAdults = (int)row[nameof(Apartment.MaxAdults)],
                        MaxChildren = (int)row[nameof(Apartment.MaxChildren)],
                        TotalRooms = (int)row[nameof(Apartment.TotalRooms)],
                        Price = (int)row[nameof(Apartment.Price)],
                        BeachDistance = (int)row[nameof(Apartment.BeachDistance)],
                        CityId = ((int)row[nameof(Apartment.CityId)]),
                        StatusName = GetStatusName((int)row[nameof(Apartment.StatusId)])

                    });
                }
            }

            return apartments;
        }

        public int GetApartmentId(string apartmentName, string address, int? rooms, int? adults, int? children)
        {
            Apartment apartment = new Apartment();

            var tblApartment = SqlHelper.ExecuteDataset(CS, nameof(GetApartmentId), apartmentName, address, rooms, adults, children).Tables[0];
            if (tblApartment.Rows.Count == 0) return 0;

            DataRow row = tblApartment.Rows[0];

            return (int)row[nameof(Apartment.Id)];
        }

        public User GetUserByID(int apartmentId)
        {
            User user = new User();

            var tblUser = SqlHelper.ExecuteDataset(CS, nameof(GetUserByID), apartmentId).Tables[0];
            if (tblUser.Rows.Count == 0) return null;

            DataRow row = tblUser.Rows[0];

            user.Id = (int)row[nameof(User.Id)];
            user.UserName = row[nameof(User.UserName)].ToString();
            user.Details = row[nameof(User.Details)].ToString();

            return user;
        }

        public User GetUnregisteredUser(int apartmentId)
        {
            User user = new User();
            var tblUser = SqlHelper.ExecuteDataset(CS, nameof(GetUnregisteredUser), apartmentId).Tables[0];
            if (tblUser.Rows.Count == 0) return null;

            DataRow row = tblUser.Rows[0];

            user.UserName = row[nameof(User.UserName)].ToString();
            user.Email = row[nameof(User.Email)].ToString();
            user.PhoneNumber = row[nameof(User.PhoneNumber)].ToString();
            user.Details = row[nameof(User.Details)].ToString();
            user.Address = row[nameof(User.Address)].ToString();

            return user;
        }


        ApartmentOwner IRepo.GetApartmentOwnerById(int apartmentId)
        {
            throw new NotImplementedException();
        }

        public ApartmentOwner GetApartmentOwnerByApartmentId(int ownerId)
        {
            ApartmentOwner apartmentOwner = new ApartmentOwner();

            var tblApartmentOwner = SqlHelper.ExecuteDataset(CS, nameof(GetApartmentOwnerByApartmentId), ownerId).Tables[0];
            if (tblApartmentOwner.Rows.Count == 0) return null;

            DataRow row = tblApartmentOwner.Rows[0];

            apartmentOwner.Id = (int)row[nameof(ApartmentOwner.Id)];
            apartmentOwner.Name = row[nameof(ApartmentOwner.Name)].ToString();

            return apartmentOwner;
        }

        public void CreateNewPublicReservation(Reservation reservation)
        {
            SqlHelper.ExecuteNonQuery(CS, nameof(CreateNewPublicReservation), reservation.ApartmentId, reservation.FirstName + " " + reservation.LastName, reservation.Email, reservation.Phone, reservation.DateFrom + " - " + reservation.DateTo + " adults: " + reservation.Adults + " children: " + reservation.Children);

        }

        int IRepo.GetReview(int apartmentId)
        {
            IList<int> numberOfReviews = new List<int>();

            var tblnumberOfStars = SqlHelper.ExecuteDataset(CS, nameof(GetReview), apartmentId).Tables[0];
            if (tblnumberOfStars.Rows.Count == 0) return 0;

            foreach (DataRow row in tblnumberOfStars.Rows)
            {
                numberOfReviews.Add((int)row["Stars"]);
            }

            int suma = 0;

            foreach (int stars in numberOfReviews)
            {
                suma += stars;
            }

            return suma / numberOfReviews.Count;
        }

        public IList<ApartmentPicture> GetApartmentPicturesByID(int apartmentId)
        {
            IList<ApartmentPicture> apartmentPictures = new List<ApartmentPicture>();

            var tblApartmentsPics = SqlHelper.ExecuteDataset(CS, nameof(GetApartmentPicturesByID), apartmentId).Tables[0];
            if (tblApartmentsPics == null) return null;

            foreach (DataRow row in tblApartmentsPics.Rows)
            {
                ApartmentPicture picture = new ApartmentPicture();
                picture.Id = (int)row[nameof(ApartmentPicture.Id)];
                picture.ApartmentId = (int)row[nameof(ApartmentPicture.ApartmentId)];
                picture.Name = row[nameof(ApartmentPicture.Name)].ToString();

                picture.IsRepresentative = row[nameof(ApartmentPicture.IsRepresentative)].ToString() == "0" ? false : true;


                picture.Path = (string)row[nameof(ApartmentPicture.Path)];

                apartmentPictures.Add(picture);
            }

            return apartmentPictures;
        }

        public void AddPictures(int apartmentId, string path)
        {
            SqlHelper.ExecuteNonQuery(CS, nameof(AddPictures), apartmentId, path);
        }

        public City GetCityByID(int id)
        {
            var tblCity = SqlHelper.ExecuteDataset(CS, nameof(GetCityByID), id).Tables[0];
            if (tblCity == null) return null;
            DataRow row = tblCity.Rows[0];

            return new City
            {
                Id = (int)row[nameof(City.Id)],
                Name = row[nameof(City.Name)].ToString()
            };
        }

        public string GetTypeNameEng(int tagID)
        {
            var tblTag = SqlHelper.ExecuteDataset(CS, nameof(GetTypeNameEng), tagID).Tables[0];
            if (tblTag.Rows.Count == 0) return null;
            DataRow row = tblTag.Rows[0];

            return row[nameof(Tag.NameEng)].ToString();
        }

        public int GetUsedTags(int tagID)
        {
            var tblUsed = SqlHelper.ExecuteDataset(CS, nameof(GetUsedTags), tagID).Tables[0];
            if (tblUsed.Rows.Count == 0) return 0;

            DataRow row = tblUsed.Rows[0];

            return (int)(row[nameof(Tag.Used)]);
        }

        IList<Tag> IRepo.GetTags()
        {
            var tblTags = SqlHelper.ExecuteDataset(CS, nameof(GetTags)).Tables[0];

            IList<Tag> tags = new List<Tag>();
            tags.Add(new Tag { Id = 0, NameEng = "--Select tag--" });
            if (tblTags.Rows.Count == 0) return null;

            foreach (DataRow row in tblTags.Rows)
            {
                var tag = new Tag();
                tag.Id = Convert.ToInt32(row[nameof(Tag.Id)]);
                tag.NameEng = row[nameof(Tag.NameEng)].ToString();
                tags.Add(tag);
            }

            return tags;
        }

        public IList<string> LoadAllTagTypes()
        {
            IList<String> tagTypes = new List<String>();

            var tblTagTypes = SqlHelper.ExecuteDataset(CS, nameof(LoadAllTagTypes)).Tables[0];
            if (tblTagTypes == null) return null;

            foreach (DataRow row in tblTagTypes.Rows)
            {
                tagTypes.Add(row[nameof(Tag.TagTypeNameEng)].ToString());
            }

            return tagTypes;
        }

        IList<Tag> IRepo.GetApartmentTags(int apartmentId)
        {
            IList<Tag> tags = new List<Tag>();

            var tblTags = SqlHelper.ExecuteDataset(CS, nameof(GetApartmentTags), apartmentId).Tables[0];
            if (tblTags == null) return null;

            foreach (DataRow row in tblTags.Rows)
            {
                tags.Add(new Tag
                {
                    Id = (int)row[nameof(Tag.Id)],
                    Name = row[nameof(Tag.Name)].ToString(),
                    NameEng = row[nameof(Tag.NameEng)].ToString(),
                    TypeId = (int)row["TagTypeID"],
                    TagTypeNameEng = row[nameof(Tag.TagTypeNameEng)].ToString()
                });
            }
            return tags;
        }

        public void AddNewTag(int typeID, string nameEng)
        {
            SqlHelper.ExecuteNonQuery(CS, nameof(AddNewTag), nameEng, typeID);
        }

        public void DeleteApartmentTagByID(int tagID, int apartmentID)
        {
            SqlHelper.ExecuteNonQuery(CS, nameof(DeleteApartmentTagByID), tagID, apartmentID);
        }

        public void AddNewTagToApartment(int tagID, int apartmentID)
        {
            Guid guid = Guid.NewGuid();
            SqlHelper.ExecuteNonQuery(CS, nameof(AddNewTagToApartment), tagID, apartmentID, guid);
        }

        public IList<Tag> GetUnusedTagsOnApartment(int apartmentId)
        {
            IList<Tag> tags = new List<Tag>();

            var tblTags = SqlHelper.ExecuteDataset(CS, nameof(GetUnusedTagsOnApartment), apartmentId).Tables[0];
            if (tblTags == null) return null;

            foreach (DataRow row in tblTags.Rows)
            {
                tags.Add(new Tag
                {
                    Id = (int)row[nameof(Tag.Id)],
                    NameEng = row[nameof(Tag.NameEng)].ToString(),
                    TypeId = (int)row[nameof(Tag.TypeId)],
                    TagTypeNameEng = row[nameof(Tag.TagTypeNameEng)].ToString(),
                });
            }
            return tags;
        }
    }

}




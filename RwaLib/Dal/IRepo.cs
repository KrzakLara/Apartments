using RwaLib.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RwaLib.Dal
{
    public interface IRepo
    {

        IList<RwaLib.Models.User> LoadUsers();
        IList<RwaLib.Models.City> LoadCities();
        IList<RwaLib.Models.Status> LoadStatus();
        IList<RwaLib.Models.Order> LoadOrder();
        IList<RwaLib.Models.ApartmentOwner> LoadOwners();
        IList<RwaLib.Models.Tag> LoadApartmentsByTagID();
      
        IList<RwaLib.Models.Apartment> AddApartment();
        IList<RwaLib.Models.Tag> AddTag();
        IList<RwaLib.Models.Apartment> GetApartments();
        IList<RwaLib.Models.Tag> GetApartmentTags();
       
        
        IList<RwaLib.Models.Apartment> LoadApartments();


        //-------------------------------------------------------------Tags-----------------------------------------
        IList<Tag> LoadTags();

        string GetTypeNameEng(int tagID);
        int GetUsedTags(int tagID);
        IList<Tag> GetTags();
        IList<string> LoadAllTagTypes();
        IList<Tag> GetApartmentTags(int apartmentId);
        void AddNewTag(int typeID, string nameEng);
        void DeleteTag(int tagId);
        void DeleteApartmentTagByID(int tagID, int apartmentID);
        void AddNewTagToApartment(int tagID, int apartmentID);
        IList<Tag> GetUnusedTagsOnApartment(int apartmentId);
        void AddTag(int tagId, string name);
        void DeleteApartmentTag(int tagIdS, int apartmentId);

        //--------------------------------------------------Apartment-----------------------------------------------
        IList<Apartment> GetAllApartments();
        Apartment GetApartmentById(int apartmentId);
        IList<Apartment> GetAllApartmentsByStatusId(int? statusId);

        IList<Apartment> CreateApartment();

        IList<Apartment> UpdateApartment();

        IList<Apartment> DeleteApartment();

        int GetApartmentId(string apartmentName, string address, int? rooms, int? adults, int? children);

        Apartment GetApartmentByID(int apartmentId);

        void AddApartment(Apartment apartment);

        //--------------------------------------------------------------User------------------------------------------

        User AuthUser(string username, string password);
        User GetUserByID(int userId);
        User GetUnregisteredUser(int apartmentId);
        void CreateNewUser(User user);


        //--------------------------------------------------------City------------------------------------------------

        IList<City> GetCities();

        City GetCityByID(int id);

        //-----------------------------------------------------Statuses--------------------------------------------
        int GetApartmentStatus(int apartmentId);
        IList<Status> GetStatuses();

        //----------------------------------------------------------Owner-------------------------------------------
        ApartmentOwner GetApartmentOwnerById(int apartmentId);
        IList<Apartment> GetApartmentOwners();
        ApartmentOwner GetApartmentOwnerByApartmentId(int ownerId);

        //-------------------------------------------------------Reservation------------------------------------------

        void CreateNewPublicReservation(Reservation reservation);

        //--------------------------------------------------------Review-----------------------------------------------
        void CreateNewReview(Review review);
        int GetReview(int apartmentId);


        //-----------------------------------------------------------Pictures--------------------------------------------
        IList<ApartmentPicture> GetApartmentPictures();
        IList<ApartmentPicture> GetApartmentPicturesByID(int apartmentId);
        void AddPictures(int apartmentId, string path);
        int GetNumberOfApartmentPictures(int apartmentId);
        void SetRepresentativePicture(int newRepresentative, int oldRepresentative);
        void DeleteApartmentPictureByID(int imageid);
        string GetImagePath(int imageId);




        //--------------------------------------------------Apartment owner-----------------------------------------------
        string GetOwnerName(int ownerId);
        

        //-----------------------------------------------------Reservation-------------------------------------------
       
        void CreateNewReservation(Reservation reservation);



       




        //public void SaveApartment(string filename, Apartment apartment);

    }
}

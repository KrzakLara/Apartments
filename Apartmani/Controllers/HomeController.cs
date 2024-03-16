using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using RwaLib.Dal;
using RwaLib.Models;

namespace Apartmani.Controllers
{
    public class HomeController : Controller
    {
        private IRepo _repo;

        public HomeController()
        {
            _repo = RepoFactory.GetRepo();
        }

        //prijava
        public ActionResult Index(string sortedBy)
        {
            IList<Apartment> Apartmani = new List<Apartment>();

            Apartmani = _repo.LoadApartments();

            IList<ApartmentPicture> apartmentPictures = new List<ApartmentPicture>();
            apartmentPictures = _repo.GetApartmentPictures().Where(p => p.IsRepresentative).ToList();


            if (sortedBy == "Default")
            {
                Apartmani = Apartmani.OrderBy(a => a.Id).ToList();
            }
            else if (sortedBy == "Asc")
            {
                Apartmani = Apartmani.OrderBy(a => a.Name).ToList();
            }
            else if (sortedBy == "Desc")
            {
                Apartmani = Apartmani.OrderByDescending(a => a.Name).ToList();
            }

            ViewBag.apartmani = Apartmani;

            ViewBag.sort = sortedBy;

            ViewBag.apartmentPictures = apartmentPictures;

            return View();
        }

        [HttpGet]
        public ActionResult ApartmentPage(int apartmentId)
        {
            IList<ApartmentPicture> apartmentPictures = _repo.GetApartmentPicturesByID(apartmentId);
            Apartment apartment = _repo.GetApartmentByID(apartmentId);

            ViewBag.apartmentPictures = apartmentPictures;

            ViewBag.apartmentId = apartmentId;

            ViewBag.apartment = apartment;

            ViewBag.tags = _repo.GetApartmentTags(apartmentId);

            ViewBag.apartmentOwner = _repo.GetApartmentOwnerById(apartment.OwnerId).ToString();

            return View();
        }

        //rezervacije apartmana
        [HttpGet]
        public ActionResult Reserve(int apartmentId)
        {
            IList<ApartmentPicture> apartmentPictures = _repo.GetApartmentPicturesByID(apartmentId);
            Apartment apartment = _repo.GetApartmentById(apartmentId);

            ViewBag.apartment = apartment;

            ViewBag.tags = _repo.GetApartmentTags(apartmentId);

            ViewBag.apartmentOwner = _repo.GetOwnerName(apartment.OwnerId);

            ViewBag.apartmentPictures = apartmentPictures;

            return View();
        }



        [HttpPost]
        public ActionResult Index(int rooms = 0, int children = 0,int adults = 0)
        {

            IList<Apartment> apartments = new List<Apartment>();
            apartments = _repo.GetApartments();

            if (rooms != 0 && adults != 0 && children != 0)
            {
                ViewBag.apartments = apartments.Where(a => a.TotalRooms == rooms && a.MaxAdults == adults && a.MaxChildren == children).ToList();
                ViewBag.apartmentPictures = _repo.GetApartmentPictures();
            }
            else if (rooms != 0 && adults != 0)
            {
                ViewBag.apartments = apartments.Where(a => a.TotalRooms == rooms && a.MaxAdults == adults).ToList();
                ViewBag.apartmentPictures = _repo.GetApartmentPictures();
            }
            else if (rooms != 0 && children != 0)
            {
                ViewBag.apartments = apartments.Where(a => a.MaxChildren == children && a.TotalRooms == rooms).ToList();
                ViewBag.apartmentPictures = _repo.GetApartmentPictures();
            }
            else if (adults != 0 && children != 0)
            {
                ViewBag.apartments = apartments.Where(a => a.MaxAdults == adults && a.MaxChildren == children).ToList();
                ViewBag.apartmentPictures = _repo.GetApartmentPictures();
            }
            else if (children != 0)
            {
                ViewBag.apartments = apartments.Where(a => a.MaxChildren == children).ToList();
                ViewBag.apartmentPictures = _repo.GetApartmentPictures();
            }
            else if (adults != 0)
            {
                ViewBag.apartments = apartments.Where(a => a.MaxAdults == adults).ToList();
                ViewBag.apartmentPictures = _repo.GetApartmentPictures();
            }
            else if (rooms != 0)
            {
                ViewBag.apartments = apartments.Where(a => a.TotalRooms == rooms).ToList();
                ViewBag.apartmentPictures = _repo.GetApartmentPictures();
            }


            return View();

        }

        //slanje rezervacije 
        [HttpPost]
        public ActionResult SendReservations(int aptartmentId, string firstname, string lastname, string email, int children, string phone, int adults)
        {
            Reservation reservation = new Reservation()
            {
                ApartmentId = aptartmentId,
                FirstName = firstname,
                LastName = lastname,
                Email = email,
                Children = children,
                Phone = phone,
                Adults = adults
            };
            _repo.CreateNewReservation(reservation);

            return RedirectToAction("Index", "Home", new { apartmentId = aptartmentId });
        }

        //slanje povratnih informacija
        [HttpPost]
        public ActionResult SendReviews(int aptId, int stars, string desc, int userId)
        {
            Review review = new Review()
            {
                ApartmentId = aptId,
                Stars = stars,
                Description = desc,
                UserId = userId
            };

            _repo.CreateNewReview(review);

            return RedirectToAction("Index", "Home", new { apartmentId = aptId });
        }
    }
}
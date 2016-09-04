using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace UnitTestsSamples.Sample2
{

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }
        public int Status { get; set; }

    }

    public interface IUserDao
    {
        User GetById(int id);

        void AddUser(User user);
    }

    public class UserController : Controller
    {
        private readonly IUserDao _userDao;
        public UserController(IUserDao userDao)
        {
            _userDao = userDao;
        }

        [HttpGet]
        public ActionResult UserInfo(int id)
        {
            var user = _userDao.GetById(id);
            if (user==null || user.Status != 1)
            {
                return RedirectToAction("Index");
            }
            return View(user);
        }

    }
}

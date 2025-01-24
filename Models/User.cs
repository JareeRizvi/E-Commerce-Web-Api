using Ecommerce_Web_API.Common;
using Ecommerce_Web_API.EntityFramework;
using Ecommerce_Web_API.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce_Web_API.Models
{
    public class User

    {
		public Ecommerce_Web_DBEntities1 db = new Ecommerce_Web_DBEntities1();
        public UserViewModel SignUp(UserViewModel model)
        {
			try
			{
				model = db.SP_SignUp(model.UserName, model.Email, CommonUser.GetMd5Hash(model.Password,model.UserName), model.Address, model.Postalcode, model.State, model.City, model.CreatedBy, model.PhoneNo)
					.Select(x => new UserViewModel
				{
					UserName = model.UserName,
					Email = model.Email,
					Password = model.Password,
					Address = model.Address,
					Postalcode = model.Postalcode,
					State = model.State,
					City = model.City,
					CreatedBy = model.CreatedBy
				}).ToList().FirstOrDefault();
					
					
			}
			catch (Exception ex)
			{

				var message = ex.Message;
				model.ResponseMessage = message;
			}
			return model;
        }


		public List<SP_Login_Result> Login(UserViewModel model)
		{
			List<SP_Login_Result> res = new List<SP_Login_Result>(); 
			try
			{
                //model = db.SP_Login(model.UserName, CommonUser.GetMd5Hash(model.Password, model.UserName))
                model = db.SP_Login(model.UserName, model.Password)
				.Select(x => new UserViewModel
					{
						UserName = model.UserName,
						Email = model.Email,
						Password = model.Password,
						Address = model.Address,
						Postalcode = model.Postalcode,
						State = model.State,
						City = model.City,
						CreatedBy = model.CreatedBy

					}).ToList().FirstOrDefault();
				model.IsSuccess= true;

			}
			catch (Exception ex) 
			{
				var message = ex.Message;
				model.ResponseMessage = message;
			}
			return new List<SP_Login_Result>();
		}
    }
}
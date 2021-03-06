﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using DMSApi.Models;
using DMSApi.Models.IRepository;
using DMSApi.Models.Repository;
using DMSApi.Models.StronglyType;

namespace DMSApi.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        private IUserRepository userRepository;

        public UserController()
        {
            this.userRepository = new UserRepository();
        }

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public HttpResponseMessage GetAllUsers()
        {
            var countries = userRepository.GetAllUsers();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, countries, formatter);
        }
        //Added by Kiron 30/10/2016
        [HttpGet, ActionName("GetAllPartyUsers")]
        public HttpResponseMessage GetAllPartyUsers()
        {
            var countries = userRepository.GetAllPartyUsers();
            var formatter = RequestFormat.JsonFormaterString();
            return Request.CreateResponse(HttpStatusCode.OK, countries, formatter);
        }
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Post([FromBody] Models.user user)
        {

            try
            {
                if (string.IsNullOrEmpty(user.full_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Party Or DBIS Name is Empty" }, formatter);

                }
                if (string.IsNullOrEmpty(user.login_name))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Login Name is Empty" }, formatter);

                }
                if (string.IsNullOrEmpty(user.password))
                {
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = "Password is Empty" }, formatter);

                }
                else
                {
                    user insertUser = new user
                    {
                        full_name = user.full_name,
                        password = user.password,
                        role_id = user.role_id,
                        login_name = user.login_name,
                        branch_id = user.branch_id,
                        party_id = user.party_id,
                        company_id = 2,
                        is_new_pass = true,
                        emp_id = user.emp_id
                    };

                    userRepository.AddUser(insertUser);
                   
                    var formatter = RequestFormat.JsonFormaterString();
                    return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "User save successfully" }, formatter);
                }


            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [System.Web.Http.HttpPut]
        public HttpResponseMessage Put([FromBody] Models.user user)
        {
            try
            {
                Models.user updateUser = new Models.user
                {
                    user_id = user.user_id,
                    full_name = user.full_name,
                    password = user.password,
                    role_id = user.role_id,
                    login_name = user.login_name,
                    branch_id = user.branch_id,
                    party_id = user.party_id,
                    company_id = 2,
                    is_new_pass = user.is_new_pass,
                    emp_id = user.emp_id
                };
               
                userRepository.EditUser(updateUser);
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "User update successfully" }, formatter);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [System.Web.Http.HttpPut]
        public HttpResponseMessage ChangeOwnProfile(UserPasswordModel userPasswordModel)
        {
            try
            {
                Confirmation aConfirmation = userRepository.ChangeOwnProfile(userPasswordModel);
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, aConfirmation, formatter);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
        }

        [System.Web.Http.HttpDelete]
        public HttpResponseMessage Delete([FromBody]Models.user user)
        {
            try
            {
                //long con_id = long.Parse(country_id);
                bool updatCountry = userRepository.DeleteUser(user.user_id);

                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "success", msg = "User Delete Successfully." }, formatter);
            }
            catch (Exception ex)
            {
                var formatter = RequestFormat.JsonFormaterString();
                return Request.CreateResponse(HttpStatusCode.OK, new Confirmation { output = "error", msg = ex.ToString() }, formatter);
            }
            
        }
        [ActionName("GetUserById")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage GetUserById([FromBody]Models.user user)
        {
            var userId = user.user_id;

            var employee = userRepository.GetUserById(userId);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, employee);
            return response;
        }
    }
}
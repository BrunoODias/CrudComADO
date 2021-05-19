using CrudADO.DAL;
using CrudADO.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CrudADO.Controllers
{
    public class PersonController : WebController
    {
        private PersonDAL _personDAL;
        public PersonController(PersonDAL personDAL)
        {
            _personDAL = personDAL;
        }

        #region List
        public IActionResult List()
        {
            var model = _personDAL.GetList();
            return View(model);
        }
        #endregion Details

        #region Details
        public IActionResult Details(int Id)
        {
            var model = _personDAL.GetDetails(Id);
            if (model == null)
                return ToViewWithError("/Views/Person/List.cshtml","Não foi possível identificar a pessoa");
            return View(model);
        }
        #endregion Details

        #region Add
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Person p)
        {
            try
            {
                _personDAL.Insert(p);
                return ToViewWithMessage("/Views/Person/List.cshtml", "Cadastrado com sucesso");
            }
            catch (CustomMessageException error) {
                return ToViewWithError("/Views/Person/List.cshtml", error.Message);
            }
            catch(Exception)
            {
                return ToViewWithError("/Views/Person/List.cshtml", "Erro ao tentar cadastrar a pessoa");
            }
        }
        #endregion Add

        #region Edit
        public IActionResult Edit(int Id)
        {
            var model = _personDAL.GetDetails(Id);
            if (model == null)
                return ToViewWithError("/Views/Person/List.cshtml", "Não foi possível identificar a pessoa");
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Person person)
        {
            try
            {
                _personDAL.Update(person);
                return ToViewWithMessage("/Views/Person/List.cshtml", "Editado com sucesso");
            }
            catch (CustomMessageException error)
            {
                return ToViewWithError("/Views/Person/List.cshtml", error.Message);
            }
            catch (Exception)
            {
                return ToViewWithError("/Views/Person/List.cshtml", "Erro ao tentar salvar a edição");
            }
        }
        #endregion Edit

        #region Delete
        public IActionResult Delete(int Id)
        {
            var model = _personDAL.GetDetails(Id);
            if (model == null)
                return ToViewWithError("/Views/Person/List.cshtml", "Não foi possível identificar a pessoa");
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(Person person)
        {
            try
            {
                _personDAL.Delete(person.Id);
                return ToViewWithMessage("/Views/Person/List.cshtml", "Excluido com sucesso");
            }
            catch (CustomMessageException error)
            {
                return ToViewWithError("/Views/Person/List.cshtml", error.Message);
            }
            catch (Exception)
            {
                return ToViewWithError("/Views/Person/List.cshtml", "Erro ao tentar excluir a pessoa");
            }
        }
        #endregion Delete
    }
}

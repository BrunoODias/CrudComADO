using CrudADO.DAL;
using CrudADO.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CrudADO.Controllers
{
    public class PersonController : Controller
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
            {
                //ViewData["Error"] = "Não foi possível encontrar a pessoa!";
                return RedirectToAction("List");
            }
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
                //ViewData["Message"] = "Pessoa cadastrada com sucesso!";
                return RedirectToAction("List");
            }
            catch (CustomMessageException error)
            {
                //ViewData["Error"] = error.Message;
                return RedirectToAction("List");
            }
            catch(Exception e)
            {
                //ViewData["Error"] = "Erro ao tentar cadastrar a pessoa!";
                return RedirectToAction("List");
            }
        }
        #endregion Add

        #region Edit
        public IActionResult Edit(int Id)
        {
            var model = _personDAL.GetDetails(Id);
            if (model == null)
            {
                ViewData["Error"] = "Não foi possível identificar a pessoa";
                return RedirectToAction("List");
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Person person)
        {
            try
            {
                _personDAL.Update(person);
                //ViewData["Message"] = "Editado com sucesso!";
                return RedirectToAction("List");
            }
            catch (CustomMessageException error)
            {
                //ViewData["Error"] = error.Message;
                return RedirectToAction("List");
            }
            catch (Exception e)
            {
                //ViewData["Error"] = "Erro ao tentar salvar a edição";
                return RedirectToAction("List");
            }
        }
        #endregion Edit

        #region Delete
        public IActionResult Delete(int Id)
        {
            var model = _personDAL.GetDetails(Id);
            if (model == null)
            {
                ViewData["Error"] = "Não foi possível identificar a pessoa";
                return RedirectToAction("List");
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult ConfirmDelete(int Id)
        {
            try
            {
                _personDAL.Delete(Id);
                //ViewData["Error"] = "Excluido com sucesso";
                return RedirectToAction("List");
            }
            catch (CustomMessageException error)
            {
                //ViewData["Error"] = error.Message;
                return RedirectToAction("List");
            }
            catch (Exception e)
            {
                //ViewData["Error"] = "Erro ao tentar excluir a pessoa";
                return RedirectToAction("List");
            }
        }
        #endregion Delete
    }
}

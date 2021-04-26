using Programming.API.Attributes;
using Programming.API.Security;
using Programming.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Programming.API.Controllers
{
    [ApiExceptionAttribute]
    public class LanguagesController : ApiController
    {
        LanguagesDAL languagesDal = new LanguagesDAL();
        [ResponseType(typeof(IEnumerable<Languages>))]
        [APIAuthorize(Roles ="A,U")]
        public IHttpActionResult Get()
        {
            var languages=languagesDal.GetLanguages();
            return Ok(languages);

        }
        [ResponseType(typeof(Languages))]

        public IHttpActionResult Get (int id)
        {
            var languages=languagesDal.GetLanguageById(id);
            if (languages == null)
            {
                return NotFound();
            }
            return Ok(languages);
        }
        [ResponseType(typeof(Languages))]
        public IHttpActionResult Post(Languages languages)
        {
            if (ModelState.IsValid)
            {
                var createdLanguage = languagesDal.CreateLanguage(languages);
                return CreatedAtRoute("DefaultApi", new { id = createdLanguage.Id }, createdLanguage);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [ResponseType(typeof(Languages))]
        public IHttpActionResult Put (int id,Languages languages)
        {
            if (languagesDal.IsThereAnyLanguage(id) == false)
            {
                return NotFound();
            }
            else if (ModelState.IsValid==false)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(languagesDal.UpdateLanguage(id, languages));
            }
        }
        public IHttpActionResult Delete(int id)
        {
            if (languagesDal.IsThereAnyLanguage(id) == false)
            {
                return NotFound();

            }
            else
            {
                languagesDal.DeleteLanguage(id);
                return StatusCode(HttpStatusCode.NoContent);
            }
        }

    }
}

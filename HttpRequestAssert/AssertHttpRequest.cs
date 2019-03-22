using Microsoft.VisualStudio.TestTools.UnitTesting;
#if NET45
using System.Web.Http.Results;
#elif NETCORE
using Microsoft.AspNetCore.Mvc;
#endif

namespace Twite.HttpRequestAssert
{
    public static class AssertHttpRequest
    {
        public static void IsNotFound(this Assert assert, object result) { test<NotFoundResult>(result); }

        public static void IsOk(this Assert assert, object result) { test<OkResult>(result); }

        public static void IsOk<T>(this Assert assert, object result) {
#if NET45
            test<OkNegotiatedContentResult<T>>(result); 
#elif NETCORE
            test<OkObjectResult>(result);
#endif
        }

        public static void IsBadRequest(this Assert assert, object result) { test<BadRequestResult>(result); }

        public static void IsBadRequestErrorMessage(this Assert assert, object result) {
#if NET45
            test<BadRequestErrorMessageResult>(result);
#elif NETCORE
            test<BadRequestObjectResult>(result);
#endif
        }

        public static void IsConflict(this Assert assert, object result) { test<ConflictResult>(result); }

#if NET45
        public static void IsInternalServerError(this Assert assert, object result) { testEither<ExceptionResult, InternalServerErrorResult>(result); }
#endif

#if NET45
        public static void IsJsonResult<T>(this Assert assert, object result) { test<JsonResult<T>>(result); }
#elif NETCORE
        public static void IsJsonResult(this Assert assert, object result) { test<JsonResult>(result); }
#endif

        public static void IsRedirect(this Assert assert, object result) { test<RedirectResult>(result); }

        public static void IsRedirectToRoute(this Assert assert, object result) { test<RedirectToRouteResult>(result); }

#if NET45
        public static void IsResponseMessage(this Assert assert, object result) { test<ResponseMessageResult>(result); }
#elif NETCORE
        public static void IsContentResult(this Assert assert, object result) { test<ContentResult>(result); }
#endif

        public static void IsStatusCode(this Assert assert, object result) { test<StatusCodeResult>(result); }

        public static void IsUnauthorized(this Assert assert, object result) { test<UnauthorizedResult>(result); }


        private static void test<T>(object value)
            where T : class
        {
            Assert.IsInstanceOfType(value, typeof(T));
        }

        private static void testEither<T, Q>(object value)
            where T : class
            where Q : class
        {
            var testTVal = value as T;
            var testQVal = value as Q;

            Assert.IsTrue(testTVal != null || testQVal != null, "Value is of neither type " + typeof(T) + " or " + typeof(Q));
        }
    }
}

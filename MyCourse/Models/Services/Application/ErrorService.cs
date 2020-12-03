using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCourse.Models.Exceptions;

namespace MyCourse.Models.Services.Application
{
    public class ErrorService
    {
        public ErrorResponse GetError(IExceptionHandlerPathFeature feature)
        {
            ErrorResponse errorResponse = new ErrorResponse();

            switch(feature.Error)
            {
                case InvalidOperationException exc:
                    errorResponse.TitlePage = "Corso non trovato";
                    errorResponse.StatusCode = 404;
                    errorResponse.ErrorView = "CourseNotFound";
                    break;
                case CourseNotFoundException exc:
                    errorResponse.TitlePage = "Corso non trovato";
                    errorResponse.StatusCode = 404;
                    errorResponse.ErrorView = "CourseNotFound";
                    break;
                default:
                    errorResponse.TitlePage = "Errore";
                    errorResponse.ErrorView = "Index";
                    break;
            }
            return errorResponse;
        }
    }
    public class ErrorResponse
    {
        public string TitlePage { get; set; }
        public int StatusCode { get; set; }
        public string ErrorView { get; set; }
    }

}
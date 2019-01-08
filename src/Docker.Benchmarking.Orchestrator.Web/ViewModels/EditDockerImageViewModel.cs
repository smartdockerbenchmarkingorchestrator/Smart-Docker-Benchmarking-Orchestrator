﻿using Docker.Benchmarking.Orchestrator.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.ViewModels
{
    public class EditDockerImageViewModel
    {
        [Display(Name = "Name", Prompt = "Unique Name for the Image")]
        //[Remote("IsNameUnique", "Docker", HttpMethod = "POST", ErrorMessage = "Name already taken, use another message.")]
        public string Name { get; set; }

        [Display(Name = "Description", Prompt = "Describe the Image Name")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Docker Image Name", Prompt = "Docker Image Name e.g.  mnee2/simplcommerce")]
        public string ImageName { get; set; }

        [Display(Name = "Docker Image Tag", Prompt = "Default is Latest")]
        public string ImageTag { get; set; }

        [Display(Name = "Image is hosted in private registry", Prompt = "e.g. Hosted in Azure, AWS Registry")]
        public bool PrivateRepository { get; set; }

        [Display(Name = "Private Registry Host", Prompt = "e.g. mnee2.dockhub.io")]
        public string PrivateRepositoryHost { get; set; }

        [Display(Name = "Private Registry Username", Prompt = "e.g. mnee2")]
        public string PrivateRepositoryUsername { get; set; }

        [Display(Name = "Private Registry Password", Prompt = "e.g. Password")]
        public string PrivateRepositoryPassword { get; set; }

        [Display(Name = "If web-application, what port to expose web-application on?", Prompt = "e.g. 80")]
        public int? ExternalPort { get; set; }

        [Display(Name = "Docker internal port", Prompt = "e.g. 80")]
        public int? InternalPort { get; set; }

        //https://docs.microsoft.com/en-us/ef/core/modeling/value-conversions
        [Display(Name = "Image Type", Prompt = "Is docker image a web-application or jmeter benchmarking for application")]
        public ImageType ImageType { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTimeOffset DateTimeCreated { get; set; }

        [Display(Name = "Active", Prompt = "Active")]
        public bool Active { get; set; }

        public List<DockerImageVariableViewModel> Variables { get; set; }
    }
}

using AutoMapper;
using Docker.Benchmarking.Orchestrator.Core.Commands.OptimizedBenchmarkExperiments.Commands;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Web.APIModels;
using Docker.Benchmarking.Orchestrator.Web.ViewModels;
using Docker.Benchmarking.Orchestrator.Web.ViewModels.AWS;

namespace Docker.Benchmarking.Orchestrator.Web
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            CreateMap<AddDockerImageViewModel, DockerImage>()
                .ForMember(dest => dest.DateTimeCreated, config => config.Ignore())
                .ReverseMap();

            CreateMap<AddDockerHostViewModel, DockerHost>()
                .ForMember(dest => dest.DateTimeCreated, config => config.Ignore())
                .ReverseMap();

            CreateMap<AddApplicationViewModel, Application>()
                .ForMember(dest => dest.DateTimeCreated, config => config.Ignore())
                .ForMember(dest => dest.ApplicationImage, config => config.Ignore())
                .ForMember(dest => dest.BenchmarkingImage, config => config.Ignore())
                .ReverseMap();

            CreateMap<EditApplicationViewModel, Application>()
               .ForMember(dest => dest.DateTimeCreated, config => config.Ignore())
               .ForMember(dest => dest.ApplicationImage, config => config.Ignore())
               .ForMember(dest => dest.BenchmarkingImage, config => config.Ignore())
               .ForMember(dest => dest.DatabaseImage, config => config.Ignore())
               .ForMember(dest => dest.TestFile, config => config.Ignore());


            CreateMap<AddBenchmarkExperimentViewModel, BenchmarkExperiment>()
                .ForMember(dest => dest.DateTimeCreated, config => config.Ignore())
                .ForMember(dest => dest.Host, config => config.Ignore())
                .ForMember(dest => dest.BenchmarkHost, config => config.Ignore())
                .ForMember(dest => dest.Application, config => config.Ignore())
                .ReverseMap();

            CreateMap<BenchmarkExperiment, EditBenchmarkExperimentViewModel>()
                .ForMember(dest => dest.Host, source => source.MapFrom(src => src.Host.Id))
                .ForMember(dest => dest.BenchmarkHost, source => source.MapFrom(src => src.BenchmarkHostId))
                .ForMember(dest => dest.Application, source => source.MapFrom(src => src.Application.Id));

            CreateMap<EditBenchmarkExperimentViewModel, BenchmarkExperiment>()
                .ForMember(dest => dest.Host, config => config.Ignore())
                .ForMember(dest => dest.BenchmarkHost, config => config.Ignore())
                .ForMember(dest => dest.Application, config => config.Ignore())
                .ReverseMap();

            CreateMap<AddApacheJmeterTestFileViewModel, ApacheJmeterTestFile>()
                .ForMember(dest => dest.DateTimeCreated, config => config.Ignore())
                .ForMember(dest => dest.FileName, source => source.MapFrom(src => src.TestUpload))
                .ReverseMap();

            CreateMap<ApacheJmeterTestFile, ApacheTestFileEditModel>()
                .ForMember(dest => dest.TestUpload, source => source.MapFrom(src => src.FileName))
                .ReverseMap();

            CreateMap<BenchmarkExperiment, StartBenchmarkExperimentViewModel>()
                .ForMember(dest => dest.Application, source => source.MapFrom(src => src.Application));

            CreateMap<AddDockerHostViewModel, AzureHost>()
                .ForMember(dest => dest.DateTimeCreated, config => config.Ignore());

            CreateMap<AddDockerHostViewModel, AWSHost>()
                .ForMember(dest => dest.DateTimeCreated, config => config.Ignore());

            //ContainerMetrics
            CreateMap<ContainerMetric, DockerStatsApiModel>()
                .ForMember(dest => dest.ApplicationId, source => source.MapFrom(src => src.BenchmarkExperiment.Id))
                .ForMember(dest => dest.DateTimeUtc, source => source.MapFrom(src => src.DateTimeCreated.ToUniversalTime()))
                .ReverseMap();


            //ContainerMetrics
            CreateMap<BenchmarkExperiment, BenchmarkTestViewModel>()
                .ForMember(dest => dest.BenchmarkExperimentId, source => source.MapFrom(src => src.Id))
                .ForMember(dest => dest.CloudProvider, source => source.MapFrom(src => src.Host.CloudProvider));



            //BenchmarkResultsJmeter
            CreateMap<BenchmarkTestItem, BenchmarkTestItemViewModel>();


            CreateMap<Application, ResultsComparisonViewModel>();

            CreateMap<DockerHost, EditDockerHostViewModel>().ReverseMap();

            CreateMap<DockerImage, EditDockerImageViewModel>().ReverseMap();

            CreateMap<ApacheJmeterTestFile, ApacheJmeterTestFileViewModel>();

            CreateMap<Application, EditApplicationViewModel>()
                .ForMember(dest => dest.BenchmarkingImage, source => source.Ignore())
                .ForMember(dest => dest.ApplicationImage, source => source.Ignore());

            CreateMap<EditApplicationViewModel, Application>()
                .ForMember(dest => dest.BenchmarkingImage, source => source.Ignore())
                .ForMember(dest => dest.ApplicationImage, source => source.Ignore());


            CreateMap<AddAzureTemplateViewModel, AzureVMTemplate>()
                .ForMember(dest => dest.DateTimeCreated, config => config.Ignore());

            CreateMap<EditAzureTemplateViewModel, AzureVMTemplate>().ReverseMap();

            CreateMap<AzureTemplateViewModel, AzureVMTemplate>();

            CreateMap<Application, ApplicationDetailsViewModel>();

            CreateMap<Application, ApplicationDetailsAPIModel>()
                .ForMember(dest => dest.BenchmarkCount, source => source.MapFrom(src => src.Benchmarks.Count));


            CreateMap<Pages.AWSCredentials.CreateModel, AWSCredentials>();
            CreateMap<Pages.AWSCredentials.EditModel, AWSCredentials>().ReverseMap();

            CreateMap<Pages.AzureCredentials.CreateModel, AzureCredientials>();
            CreateMap<Pages.AzureCredentials.EditModel, AzureCredientials>().ReverseMap();

            CreateMap<AWSParameterViewModel, Amazon.CloudFormation.Model.Parameter>()
                .ForMember(dest => dest.ParameterKey, source => source.MapFrom(src => src.Key))
                .ForMember(dest => dest.ParameterValue, source => source.MapFrom(src => src.Value))
                .ReverseMap();


            CreateMap<Pages.AWS.CreateModel, AWSCloudFormationTemplate>()
                .ForMember(dest => dest.Parameters, source => source.MapFrom(src => src.Parameters));

            CreateMap<Pages.AWS.EditModel, AWSCloudFormationTemplate>()
                .ForMember(dest => dest.Parameters, source => source.MapFrom(src => src.Parameters))
                .ReverseMap();

            CreateMap<AWSParameterViewModel, AWSCloudFormationParameter>()
                .ForMember(dest => dest.ParameterKey, source => source.MapFrom(src => src.Key))
                .ForMember(dest => dest.ParameterValue, source => source.MapFrom(src => src.Value))
                .ReverseMap();

            CreateMap<DockerImageVariable, DockerImageVariableViewModel>().ReverseMap();
            CreateMap<ApplicationTestVariable, ApplicationTestVariableViewModel>().ReverseMap();
            CreateMap<BenchmarkExperimentVariable, BenchmarkExperimentVariableViewModel>()
                .ForMember(dest => dest.Name, source => source.MapFrom(src => src.Name.Trim()))
                .ForMember(dest => dest.Value, source => source.MapFrom(src => src.Value.Trim()))
                .ReverseMap();

            //AWS ParametersMapping
            CreateMap<AWSCloudFormationParameter, Amazon.CloudFormation.Model.Parameter>()
                .ForMember(dest => dest.ParameterKey, source => source.MapFrom(src => src.ParameterKey))
                .ForMember(dest => dest.ParameterValue, source => source.MapFrom(src => src.ParameterValue))
                .ReverseMap();


            CreateMap<OptimisedBenchmarkExperimentRequestViewModel, CreateOptimizedExperimentCommand>();
        }
    }
}

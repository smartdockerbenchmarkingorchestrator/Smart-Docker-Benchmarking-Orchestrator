using Docker.Benchmarking.Orchestrator.Core.Commands;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using Docker.Benchmarking.Orchestrator.Core.SharedKernel;
using MediatR;
using StructureMap;
using StructureMap.Graph;
using StructureMap.Graph.Scanning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web
{
    public class AddRequestHandlersWithGenericParametersToRegistry : IRegistrationConvention
    {
        public void ScanTypes(TypeSet types, Registry registry)
        {
            foreach (var concreteClass in types.FindTypes(TypeClassification.Concretes))
            {
                if (concreteClass.GetInterfaces().Contains(typeof(IBaseEntity)))
                {
                    var listType = typeof(IEnumerable<>).MakeGenericType(concreteClass);
                    var iQueryableType = typeof(IQueryable<>).MakeGenericType(concreteClass);

                    var genericCreateCommand = typeof(CreateCommand<>).MakeGenericType(concreteClass);
                    var interfaceCreateCommandHandlerType = typeof(IRequestHandler<,>).MakeGenericType(genericCreateCommand, concreteClass);
                    var concreteHandlerTypeCreate = typeof(CreateCommandHandler<>).MakeGenericType(concreteClass);
                    registry.For(interfaceCreateCommandHandlerType).Use(concreteHandlerTypeCreate);

                    var genericUpdateCommand = typeof(UpdateCommand<>).MakeGenericType(concreteClass);
                    var interfaceUpdateCommandHandlerType = typeof(IRequestHandler<,>).MakeGenericType(genericUpdateCommand, concreteClass);
                    var concreteHandlerTypeUpdate = typeof(UpdateCommandHandler<>).MakeGenericType(concreteClass);
                    registry.For(interfaceUpdateCommandHandlerType).Use(concreteHandlerTypeUpdate);

                    var genericDeleteCommand = typeof(DeleteEntityCommand<>).MakeGenericType(concreteClass);
                    var interfaceDeleteCommandHandlerType = typeof(IRequestHandler<,>).MakeGenericType(genericDeleteCommand, typeof(bool));
                    var concreteHandlerTypeDelete = typeof(DeleteEntityCommandHandler<>).MakeGenericType(concreteClass);
                    registry.For(interfaceDeleteCommandHandlerType).Use(concreteHandlerTypeDelete);

                    var genericEntityCommand = typeof(GetEntityCommand<>).MakeGenericType(concreteClass);
                    var interfaceEntityCommandHandlerType = typeof(IRequestHandler<,>).MakeGenericType(genericEntityCommand, concreteClass);
                    var concreteHandlerTypeEntity = typeof(GetEntityCommandHandler<>).MakeGenericType(concreteClass);
                    registry.For(interfaceEntityCommandHandlerType).Use(concreteHandlerTypeEntity);

                    var genericEntitiesCommand = typeof(GetEntitiesCommand<>).MakeGenericType(concreteClass);
                    var interfaceEntitiesCommandHandlerType = typeof(IRequestHandler<,>).MakeGenericType(genericEntitiesCommand, listType);
                    var concreteHandlerTypeEntities = typeof(GetEntitiesCommandHandler<>).MakeGenericType(concreteClass);
                    registry.For(interfaceEntitiesCommandHandlerType).Use(concreteHandlerTypeEntities);


                    var listActiveEntitiesCommand = typeof(ListActiveEntitiesCommand<>).MakeGenericType(concreteClass);
                    var interfaceListActiveCommandHandlerType = typeof(IRequestHandler<,>).MakeGenericType(listActiveEntitiesCommand, listType);

                    var concreteHandlerTypeListActive = typeof(ListActiveEntitiesCommandHandler<>).MakeGenericType(concreteClass);
                    registry.For(interfaceListActiveCommandHandlerType).Use(concreteHandlerTypeListActive);

                    var listEntitiesCommand = typeof(ListEntitiesCommand<>).MakeGenericType(concreteClass);
                    var interfaceListCommandHandlerType = typeof(IRequestHandler<,>).MakeGenericType(listEntitiesCommand, listType);
                    var concreteHandlerTypeList = typeof(ListEntitiesCommandHandler<>).MakeGenericType(concreteClass);
                    registry.For(interfaceListCommandHandlerType).Use(concreteHandlerTypeList);

                    var rangeEntitiesCommand = typeof(AddRangeEntitiesCommand<>).MakeGenericType(concreteClass);
                    var interfaceRangeEntitiesCommandHandlerType = typeof(IRequestHandler<,>).MakeGenericType(rangeEntitiesCommand, typeof(bool));
                    var concreteHandlerTypeRangeEntities = typeof(AddRangeEntitiesCommandHandler<>).MakeGenericType(concreteClass);
                    registry.For(interfaceRangeEntitiesCommandHandlerType).Use(concreteHandlerTypeRangeEntities);

                    var findEntitiesCommand = typeof(FindEntitiesCommand<>).MakeGenericType(concreteClass);
                    var interfaceFindEntitiesCommandHandlerType = typeof(IRequestHandler<,>).MakeGenericType(findEntitiesCommand, iQueryableType);
                    var concreteHandlerTypeFindEntities = typeof(FindEntitiesCommandHandler<>).MakeGenericType(concreteClass);
                    registry.For(interfaceFindEntitiesCommandHandlerType).Use(concreteHandlerTypeFindEntities);

                    var findByNameCommand = typeof(FindEntityByNameCommand<>).MakeGenericType(concreteClass);
                    var interfaceFindEntitiesByNameCommandHandlerType = typeof(IRequestHandler<,>).MakeGenericType(findByNameCommand, typeof(bool));
                    var concreteHandlerTypeFindEntitiesByName = typeof(FindEntityByNameCommandHandler<>).MakeGenericType(concreteClass);
                    registry.For(interfaceFindEntitiesByNameCommandHandlerType).Use(concreteHandlerTypeFindEntitiesByName);


                }
            }
        }
    }
}

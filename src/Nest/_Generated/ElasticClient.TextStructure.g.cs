// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
//
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// ------------------------------------------------
//
// This file is automatically generated.
// Please do not edit these files manually.
// Run the following in the root of the repository:
//
// ------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;

#nullable restore
namespace Nest.TextStructure
{
	public class TextStructureNamespace : NamespacedClientProxy
	{
		internal TextStructureNamespace(ElasticClient client) : base(client)
		{
		}

		public FindStructureResponse FindStructure<TJsonDocument>(IFindStructureRequest<TJsonDocument> request)
		{
			return DoRequest<IFindStructureRequest<TJsonDocument>, FindStructureResponse>(request, request.RequestParameters);
		}

		public Task<FindStructureResponse> FindStructureAsync<TJsonDocument>(IFindStructureRequest<TJsonDocument> request, CancellationToken cancellationToken = default)
		{
			return DoRequestAsync<IFindStructureRequest<TJsonDocument>, FindStructureResponse>(request, request.RequestParameters, cancellationToken);
		}

		public FindStructureResponse FindStructure<TJsonDocument>(TJsonDocument jsonDocument, Func<FindStructureDescriptor<TJsonDocument>, IFindStructureRequest<TJsonDocument>> selector = null)
		{
			return FindStructure<TJsonDocument>(selector.InvokeOrDefault(new FindStructureDescriptor<TJsonDocument>()));
		}

		public Task<FindStructureResponse> FindStructureAsync<TJsonDocument>(TJsonDocument jsonDocument, Func<FindStructureDescriptor<TJsonDocument>, IFindStructureRequest<TJsonDocument>> selector = null, CancellationToken cancellationToken = default)
		{
			return FindStructureAsync<TJsonDocument>(selector.InvokeOrDefault(new FindStructureDescriptor<TJsonDocument>()), cancellationToken);
		}
	}
}
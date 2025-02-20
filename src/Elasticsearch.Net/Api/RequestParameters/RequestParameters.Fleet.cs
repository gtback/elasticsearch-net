// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗  
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝  
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// -----------------------------------------------
//  
// This file is automatically generated 
// Please do not edit these files manually
// Run the following in the root of the repos:
//
// 		*NIX 		:	./build.sh codegen
// 		Windows 	:	build.bat codegen
//
// -----------------------------------------------
// ReSharper disable RedundantUsingDirective
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Elastic.Transport;

// ReSharper disable once CheckNamespace
namespace Elasticsearch.Net.Specification.FleetApi
{
	///<summary>Request options for GlobalCheckpoints <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/get-global-checkpoints.html</para></summary>
	public class GlobalCheckpointsRequestParameters : RequestParameters<GlobalCheckpointsRequestParameters>
	{
		///<summary>Comma separated list of checkpoints</summary>
		public string[] Checkpoints
		{
			get => Q<string[]>("checkpoints");
			set => Q("checkpoints", value);
		}

		///<summary>Timeout to wait for global checkpoint to advance</summary>
		public TimeSpan Timeout
		{
			get => Q<TimeSpan>("timeout");
			set => Q("timeout", value);
		}

		///<summary>Whether to wait for the global checkpoint to advance past the specified current checkpoints</summary>
		public bool? WaitForAdvance
		{
			get => Q<bool? >("wait_for_advance");
			set => Q("wait_for_advance", value);
		}

		///<summary>Whether to wait for the target index to exist and all primary shards be active</summary>
		public bool? WaitForIndex
		{
			get => Q<bool? >("wait_for_index");
			set => Q("wait_for_index", value);
		}
	}
}
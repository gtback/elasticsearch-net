/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System.Collections.Generic;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(SimulatedActionsFormatter))]
	public class SimulatedActions
	{
		private SimulatedActions() { }

		public IEnumerable<string> Actions { get; private set; }

		public static SimulatedActions All => new SimulatedActions { UseAll = true };
		public bool UseAll { get; private set; }

		public static SimulatedActions Some(params string[] actions) => new SimulatedActions { Actions = actions };

		public static SimulatedActions Some(IEnumerable<string> actions) => new SimulatedActions { Actions = actions };
	}

	internal class SimulatedActionsFormatter : IJsonFormatter<SimulatedActions>
	{
		public SimulatedActions Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() == JsonToken.String)
				return SimulatedActions.All;

			var formatter = formatterResolver.GetFormatter<IEnumerable<string>>();
			return SimulatedActions.Some(formatter.Deserialize(ref reader, formatterResolver));
		}

		public void Serialize(ref JsonWriter writer, SimulatedActions value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null) return;

			if (value.UseAll) writer.WriteString("_all");
			else
			{
				var formatter = formatterResolver.GetFormatter<IEnumerable<string>>();
				formatter.Serialize(ref writer, value.Actions, formatterResolver);
			}
		}
	}
}

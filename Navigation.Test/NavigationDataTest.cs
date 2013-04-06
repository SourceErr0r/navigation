﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Web;
using System.Web.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Navigation.Test
{
	[TestClass]
	public class NavigationDataTest
	{
		public NavigationDataTest()
		{
		}

		private TestContext testContextInstance;

		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		[TestInitialize]
		public void Initialize()
		{
			StateContext.Data.Clear();
		}

		private NavigationData _IndividualNavigationData;
		private NavigationData IndividualNavigationData
		{
			get
			{
				if (_IndividualNavigationData == null)
				{
					_IndividualNavigationData = new NavigationData();
					_IndividualNavigationData["string"] = "Hello";
					_IndividualNavigationData["bool"] = true;
					_IndividualNavigationData["int"] = (int)0;
					_IndividualNavigationData["short"] = (short)1;
					_IndividualNavigationData["long"] = (long)2;
					_IndividualNavigationData["float"] = (float)3;
					_IndividualNavigationData["double"] = (double)4;
					_IndividualNavigationData["decimal"] = (decimal)5;
					_IndividualNavigationData.Bag.DateTime = new DateTime(1990, 3, 1, 12, 35, 47);
					_IndividualNavigationData.Bag.TimeSpan = new TimeSpan(10, 5, 23);
					_IndividualNavigationData["byte"] = (byte)6;
					_IndividualNavigationData["char"] = '7';
					_IndividualNavigationData["Guid"] = Guid.Empty;
					_IndividualNavigationData.Bag.customData = new CustomData() { Name = "Bob", Age = 18 };
					_IndividualNavigationData.Bag.custom2Data = new Custom2Data() { Name = "Bob", Age = 18 };
					_IndividualNavigationData.Bag.dayOfWeek = DayOfWeek.Saturday;
				}
				return _IndividualNavigationData;
			}
		}

		private NavigationData _ListNavigationData;
		private NavigationData ListNavigationData
		{
			get
			{
				if (_ListNavigationData == null)
				{
					_ListNavigationData = new NavigationData();
					_ListNavigationData.Bag.List_string = new List<string> { "He-llo", "World" };
					_ListNavigationData.Bag.List_bool = new List<bool> { true, false };
					_ListNavigationData.Bag.List_short = new List<short> { 1, 2 };
					_ListNavigationData.Bag.List_long = new List<long> { 2, 3 };
					_ListNavigationData.Bag.List_float = new List<float> { 3, 4 };
					_ListNavigationData["List_double"] = new List<double> { 4, 5 };
					_ListNavigationData["List_decimal"] = new List<decimal> { 5, 6 };
					_ListNavigationData["List_DateTime"] = new List<DateTime>() { new DateTime(1990, 3, 1, 12, 35, 47), new DateTime(1971, 10, 5, 11, 1, 15) };
					_ListNavigationData["List_TimeSpan"] = new List<TimeSpan>() { new TimeSpan(10, 5, 23), new TimeSpan(8, 7, 6) };
					_ListNavigationData["List_byte"] = new List<byte>() { 6, 7 };
					_ListNavigationData["List_char"] = new List<char>() { '7', '8' };
					_ListNavigationData["List_Guid"] = new List<Guid>() { Guid.Empty, Guid.Empty };
					_ListNavigationData["List_customData"] = new List<CustomData>() { new CustomData() { Name = "Bob", Age = 18 }, new CustomData() { Name = "Jane", Age = 20 } };
					_ListNavigationData["List_custom2Data"] = new List<Custom2Data>() { new Custom2Data() { Name = "B--ob", Age = 18 }, new Custom2Data() { Name = "Jane", Age = 20 } };
					_ListNavigationData["List_dayOfWeek"] = new List<DayOfWeek>() { DayOfWeek.Saturday, DayOfWeek.Monday };
				}
				return _ListNavigationData;
			}
		}

		private NavigationData _ArrayListNavigationData;
		private NavigationData ArrayListNavigationData
		{
			get
			{
				if (_ArrayListNavigationData == null)
				{
					_ArrayListNavigationData = new NavigationData();
					_ArrayListNavigationData["ArrayList_string"] = new ArrayList() { null, "Wor-ld" };
					_ArrayListNavigationData["ArrayList_bool"] = new ArrayList() { true, false };
					_ArrayListNavigationData["ArrayList_short"] = new ArrayList() { 1, 2 };
					_ArrayListNavigationData["ArrayList_long"] = new ArrayList() { 2, 3 };
					_ArrayListNavigationData["ArrayList_float"] = new ArrayList() { 3, 4 };
					_ArrayListNavigationData.Bag.ArrayList_double = new ArrayList() { 4, 5 };
					_ArrayListNavigationData.Bag.ArrayList_decimal = new ArrayList() { 5, 6 };
					_ArrayListNavigationData["ArrayList_DateTime"] = new ArrayList() { new DateTime(1990, 3, 1, 12, 35, 47), new DateTime(1971, 10, 5, 11, 1, 15) };
					_ArrayListNavigationData["ArrayList_TimeSpan"] = new ArrayList() { new TimeSpan(10, 5, 23), new TimeSpan(8, 7, 6) };
					_ArrayListNavigationData.Bag.ArrayList_byte = new ArrayList() { 6, 7 };
					_ArrayListNavigationData.Bag.ArrayList_char = new ArrayList() { '7', '8' };
					_ArrayListNavigationData.Bag.ArrayList_Guid = new ArrayList() { Guid.Empty, Guid.Empty };
					_ArrayListNavigationData["ArrayList_customData"] = new ArrayList() { new CustomData() { Name = "Bo-b", Age = 18 }, new CustomData() { Name = "Jane", Age = 20 } };
					_ArrayListNavigationData["ArrayList_custom2Data"] = new ArrayList() { new Custom2Data() { Name = "Bob", Age = 18 }, new Custom2Data() { Name = "Jane", Age = 20 } };
					_ArrayListNavigationData["ArrayList_dayOfWeek"] = new ArrayList() { DayOfWeek.Saturday, DayOfWeek.Monday };
				}
				return _ArrayListNavigationData;
			}
		}

		[TestMethod]
		public void NavigateIndividualDataTest()
		{
			StateController.Navigate("d0", IndividualNavigationData);
			int i = 0;
			foreach (NavigationDataItem item in StateContext.Data)
			{
				Assert.AreEqual(IndividualNavigationData[item.Key], item.Value);
				i++;
			}
			Assert.AreEqual(StateContext.Bag.dayOfWeek, DayOfWeek.Saturday);
			Assert.AreEqual(16, i);
		}

		[TestMethod]
		public void NavigateIndividualDataWithoutTrailTest()
		{
			StateController.Navigate("d2");
			StateController.Navigate("t0");
			StateController.Navigate("t0", IndividualNavigationData);
			int i = 0;
			foreach (NavigationDataItem item in StateContext.Data)
			{
				Assert.AreEqual(IndividualNavigationData[item.Key], item.Value);
				i++;
			}
			Assert.AreEqual(StateContext.Bag.DateTime, new DateTime(1990, 3, 1, 12, 35, 47));
			Assert.AreEqual(StateContext.Bag.TimeSpan, new TimeSpan(10, 5, 23));
			Assert.AreEqual(16, i);
		}

		[TestMethod]
		public void NavigateListDataTest()
		{
			StateController.Navigate("d0", ListNavigationData);
			StateController.Navigate("t0");
			StateController.NavigateBack(1);
			int i = 0;
			foreach (NavigationDataItem item in (IEnumerable)StateContext.Data)
			{
				Assert.AreEqual(((IList)ListNavigationData[item.Key])[0], ((IList)item.Value)[0]);
				Assert.AreEqual(((IList)ListNavigationData[item.Key])[1], ((IList)item.Value)[1]);
				i++;
			}
			Assert.AreEqual(StateContext.Bag.List_float[0], 3f);
			Assert.AreEqual(StateContext.Bag.List_double[1], 5d);
			Assert.AreEqual(15, i);
		}

		[TestMethod]
		public void NavigateArrayListDataTest()
		{
			StateController.Navigate("d0", ArrayListNavigationData);
			StateController.Navigate("t0");
			StateController.NavigateBack(1);
			int i = 0;
			foreach (NavigationDataItem item in StateContext.Data)
			{
				Assert.AreEqual(((IList)ArrayListNavigationData[item.Key])[0], ((IList)item.Value)[0]);
				Assert.AreEqual(((IList)ArrayListNavigationData[item.Key])[1], ((IList)item.Value)[1]);
				i++;
			}
			Assert.AreEqual(StateContext.Bag.ArrayList_string[0], null);
			Assert.AreEqual(StateContext.Bag.ArrayList_string.Count, 2);
			Assert.AreEqual(StateContext.Bag.ArrayList_bool[1], false);
			Assert.AreEqual(StateContext.Bag.ArrayList_Guid[0], Guid.Empty);
			Assert.AreEqual(15, i);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void InvalidIndividualDataTest()
		{
			NavigationData data = new NavigationData();
			data["item"] = DateTimeOffset.MinValue;
			StateController.Navigate("d0", data);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void InvalidListDataTest()
		{
			StateController.Navigate("d0");
			StateContext.Data.Bag.item = new List<DateTimeKind>() { DateTimeKind.Local };
			StateController.Navigate("t0");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void InvalidListTest()
		{
			NavigationData data = new NavigationData();
			data.Bag.item = new HashSet<string>() { "1" };
			StateController.Navigate("d0", data);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void InvalidArrayListDataTest()
		{
			StateController.Navigate("d0");
			StateContext.Data["item"] = new ArrayList() { DateTimeKind.Local };
			StateController.Navigate("t0");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void InvalidDataGetNavigationLinkTest()
		{
			NavigationData data = new NavigationData();
			data["item"] = DateTimeOffset.MinValue;
			string link = StateController.GetNavigationLink("d0", data);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void InvalidDataRefreshTest()
		{
			StateController.Navigate("d0");
			StateContext.Data.Bag.item = DateTimeOffset.MinValue;
			StateController.Refresh(new NavigationData(true));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void InvalidRefreshDataTest()
		{
			StateController.Navigate("d0");
			StateController.Refresh(new NavigationData() { { "item", DateTimeOffset.MinValue } });
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void InvalidDataGetRefreshLinkTest()
		{
			StateController.Navigate("d0");
			StateContext.Data["item"] = DateTimeOffset.MinValue;
			string link = StateController.GetRefreshLink(new NavigationData(true));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void InvalidGetRefreshLinkDataTest()
		{
			StateController.Navigate("d0");
			string link = StateController.GetRefreshLink(new NavigationData() { { "item", DateTimeOffset.MinValue } });
		}

		[TestMethod]
		public void InvalidTypesArrayListDataTest()
		{
			NavigationData data = new NavigationData();
			data.Bag.item0 = new ArrayList() { "0", 1 };
			data["item1"] = new ArrayList() { 0, "1" };
			StateController.Navigate("d0", data);
			Assert.AreEqual(((ArrayList)StateContext.Data["item0"])[0], "0");
			Assert.AreEqual(StateContext.Bag.item0[1], "1");
			Assert.AreEqual(StateContext.Bag.item1[0], 0);
			Assert.AreEqual(((ArrayList)StateContext.Data["item1"])[1], 1);
		}

		[TestMethod]
		public void NavigateInvalidContextDataWithoutTrailTest()
		{
			StateController.Navigate("d2");
			StateController.Navigate("t0");
			NavigationData data = new NavigationData();
			data["s"] = "Hello";
			StateContext.Data["item"] = DateTimeOffset.MinValue;
			StateController.Navigate("t0", data);
			Assert.AreEqual("Hello", StateContext.Bag.s);
		}

		[TestMethod]
		public void RefreshInvalidContextDataTest()
		{
			StateController.Navigate("d0");
			NavigationData data = new NavigationData();
			data.Bag.s = "Hello";
			StateContext.Data["item"] = DateTimeOffset.MinValue;
			StateController.Refresh(data);
			Assert.AreEqual("Hello", StateContext.Data["s"]);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void NavigateInvalidDataWithoutTrailTest()
		{
			NavigationData data = new NavigationData();
			data["item"] = DateTimeOffset.MinValue;
			StateController.Navigate("d2", data);
		}

		[TestMethod]
		public void ReservedUrlCharacterDataTest()
		{
			NavigationData data = new NavigationData();
			data["*=/()-_+~@:?><.;[]{}!£$%^&"] = "!=/£$%^&*()-_+~@:?><.;[]{}";
			StateController.Navigate("d0", data);
			StateController.Navigate("t0");
			StateController.NavigateBack(1);
			Assert.AreEqual("!=/£$%^&*()-_+~@:?><.;[]{}", StateContext.Data["*=/()-_+~@:?><.;[]{}!£$%^&"]);
		}

		[TestMethod]
		public void SeparatorUrlCharacterDataTest()
		{
			NavigationData data = new NavigationData();
			data["_0_1_2_3_4_5_"] = "__00__11__22__33__44__55__";
			StateController.Navigate("d0", data);
			StateController.Navigate("t0");
			StateController.NavigateBack(1);
			Assert.AreEqual("__00__11__22__33__44__55__", StateContext.Data["_0_1_2_3_4_5_"]);
		}

		[TestMethod]
		public void EmptyStringDataNavigateTest()
		{
			NavigationData data = new NavigationData();
			data.Bag.s = "";
			data.Bag.t = "1";
			StateController.Navigate("d0", data);
			Assert.IsNull(StateContext.Data["s"]);
			Assert.AreEqual("1", StateContext.Data["t"]);
		}

		[TestMethod]
		public void EmptyStringDataTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			StateContext.Data["s"] = "";
			StateContext.Data["t"] = "1";
			Assert.AreEqual("", StateContext.Data["s"]);
			Assert.AreEqual("1", StateContext.Data["t"]);
		}

		[TestMethod]
		public void EmptyStringStateDataNavigateBackTest()
		{
			StateController.Navigate("d0");
			StateContext.Data["s"] = "";
			StateContext.Data["t"] = "1";
			StateController.Navigate("t0");
			StateController.NavigateBack(1);
			Assert.IsNull(StateContext.Data["s"]);
			Assert.AreEqual("1", StateContext.Data["t"]);
		}

		[TestMethod]
		public void NavigateDataNavigateBackTest()
		{
			NavigationData data = new NavigationData();
			data.Bag.s = "Hello";
			data.Bag.t = string.Empty;
			StateController.Navigate("d0", data);
			StateController.Navigate("t0");
			Assert.AreEqual("Hello", StateController.Crumbs[0].Data.Bag.s);
			Assert.IsNull(StateController.Crumbs[0].Data.Bag.t);
			StateController.NavigateBack(1);
			Assert.AreEqual("Hello", StateContext.Data["s"]);
			Assert.IsNull(StateContext.Data["t"]);
		}

		[TestMethod]
		public void ChangeDataNavigateBackTest()
		{
			NavigationData data = new NavigationData();
			data["s"] = "Hello";
			StateController.Navigate("d0", data);
			StateContext.Data["s"] = "World";
			StateContext.Data.Bag.i = 2;
			StateController.Navigate("t0");
			Assert.AreEqual("World", StateController.Crumbs[0].Data.Bag.s);
			Assert.AreEqual(2, StateController.Crumbs[0].Data["i"]);
			StateController.NavigateBack(1);
			Assert.AreEqual("World", StateContext.Data["s"]);
			Assert.AreEqual(2, StateContext.Data.Bag.i);
		}

		[TestMethod]
		public void BlankDataNavigateBackTest()
		{
			NavigationData data = new NavigationData();
			data["s"] = "Hello";
			StateController.Navigate("d0", data);
			StateContext.Bag.s = null;
			StateContext.Data["i"] = 2;
			StateController.Navigate("t0");
			Assert.IsNull(StateController.Crumbs[0].Data["s"]);
			Assert.AreEqual(2, StateController.Crumbs[0].Bag.i);
			StateController.NavigateBack(1);
			Assert.AreEqual(null, StateContext.Bag.s);
			Assert.AreEqual(2, StateContext.Data["i"]);
		}

		[TestMethod]
		public void ClearDataNavigateBackTest()
		{
			NavigationData data = new NavigationData();
			data["s"] = "Hello";
			StateController.Navigate("d0", data);
			StateContext.Data.Clear();
			StateController.Navigate("t0");
			Assert.IsNull(StateController.Crumbs[0].Bag.s);
			Assert.IsNull(StateController.Crumbs[0].Data["i"]);
			StateController.NavigateBack(1);
			Assert.AreEqual(null, StateContext.Data["s"]);
			Assert.AreEqual(null, StateContext.Data["i"]);
		}

		[TestMethod]
		public void RemoveDataNavigateBackTest()
		{
			NavigationData data = new NavigationData();
			data["s"] = "Hello";
			StateController.Navigate("d0", data);
			StateContext.Data.Remove("s");
			StateContext.Bag.i = 2;
			StateController.Navigate("t0");
			Assert.IsNull(StateController.Crumbs[0].Data["s"]);
			Assert.AreEqual(2, StateController.Crumbs[0].Bag.i);
			StateController.NavigateBack(1);
			Assert.IsNull(StateContext.Data["s"]);
			Assert.AreEqual(2, StateContext.Data["i"]);
		}

		[TestMethod]
		public void NavigateDataRefreshTest()
		{
			StateController.Navigate("d0");
			NavigationData data = new NavigationData();
			data["s"] = "Hello";
			StateController.Navigate("t0", data);
			StateController.Refresh(new NavigationData(true));
			Assert.AreEqual("Hello", StateContext.Data["s"]);
		}

		[TestMethod]
		public void NavigateRefreshDataTest()
		{
			StateController.Navigate("d0");
			NavigationData data = new NavigationData();
			data["s"] = "Hello";
			StateController.Navigate("t0");
			StateController.Refresh(data);
			Assert.AreEqual("Hello", StateContext.Data["s"]);
		}

		[TestMethod]
		public void NavigateDataRefreshDataOverrideTest()
		{
			StateController.Navigate("d0");
			NavigationData data = new NavigationData();
			data["s"] = "Hello";
			StateController.Navigate("t0", data);
			data = new NavigationData();
			data["s"] = "World";
			StateController.Refresh(data);
			Assert.AreEqual("World", StateContext.Data["s"]);
		}

		[TestMethod]
		public void NavigateDataRefreshDataBlankTest()
		{
			StateController.Navigate("d0");
			NavigationData data = new NavigationData();
			data["s"] = "Hello";
			StateController.Navigate("t0", data);
			StateController.Refresh();
			Assert.IsNull(StateContext.Data["s"]);
		}

		[TestMethod]
		public void NavigateDataRefreshDataClearTest()
		{
			StateController.Navigate("d0");
			NavigationData data = new NavigationData();
			data["s"] = "Hello";
			StateController.Navigate("t0", data);
			StateContext.Data.Clear();
			StateController.Refresh(new NavigationData(true));
			Assert.IsNull(StateContext.Bag.s);
		}

		[TestMethod]
		public void ChangeDataRefreshTest()
		{
			StateController.Navigate("d0");
			NavigationData data = new NavigationData();
			data["s"] = "Hello";
			StateController.Navigate("t0", data);
			StateContext.Bag.s = "World";
			StateContext.Data["d"] = new DateTime(2000, 1, 3);
			StateController.Refresh(new NavigationData(true));
			Assert.AreEqual("World", StateContext.Data["s"]);
			Assert.AreEqual(new DateTime(2000, 1, 3), StateContext.Bag.d);
		}

		[TestMethod]
		public void ChangeRefreshDataTest()
		{
			StateController.Navigate("d0");
			NavigationData data = new NavigationData();
			data["s"] = "Hello";
			data["i"] = 3;
			StateController.Navigate("t0", data);
			data = new NavigationData();
			data["s"] = "World";
			data["d"] = new DateTime(2000, 1, 3);
			StateController.Refresh(data);
			Assert.AreEqual("World", StateContext.Data["s"]);
			Assert.AreEqual(new DateTime(2000, 1, 3), StateContext.Data["d"]);
			Assert.IsNull(StateContext.Data["i"]);
		}

		[TestMethod]
		public void ChangeDynamicDataRefreshDataOverrideTest()
		{
			StateController.Navigate("d0");
			NavigationData data = new NavigationData();
			data.Bag.s = "Hello";
			StateController.Navigate("t0", data);
			StateContext.Bag.s = "World";
			StateContext.Bag.d = new DateTime(2000, 1, 3);
			StateContext.Bag.i = 3;
			data = new NavigationData(true);
			data.Bag.s = "Hello World";
			data.Bag.i = null;
			data.Bag.n = 2;
			StateController.Refresh(data);
			Assert.AreEqual("Hello World", StateContext.Bag.s);
			Assert.AreEqual(new DateTime(2000, 1, 3), StateContext.Bag.d);
			Assert.IsNull(StateContext.Data.Bag.i);
			Assert.AreEqual(2, StateContext.Data.Bag.n);
		}

		[TestMethod]
		public void NavigateWizardDataTest()
		{
			StateController.Navigate("d0");
			NavigationData data = new NavigationData(){
				{"s","Hello"},
				{"n",5d}
			};
			StateController.Navigate("t0", data);
			StateController.Navigate("t1", new NavigationData(true));
			Assert.AreEqual("Hello", StateController.Crumbs[1]["s"]);
			Assert.AreEqual(5d, StateController.Crumbs[1]["n"]);
			Assert.AreEqual("Hello", StateContext.Data["s"]);
			Assert.AreEqual(5d, StateContext.Data["n"]);
		}

		[TestMethod]
		public void NavigateDataNavigateTransitionTransitionTest()
		{
			NavigationData data = new NavigationData();
			data["s"] = 1;
			StateController.Navigate("d0", data);
			Assert.AreEqual(1, StateContext.Data["s"]);
			Assert.IsNull(StateContext.Data["t"]);
			data["s"] = 2;
			data["t"] = "2";
			StateController.Navigate("t1", data);
			Assert.AreEqual(1, StateController.Crumbs[0]["s"]);
			Assert.IsNull(StateController.Crumbs[0]["t"]);
			Assert.AreEqual(2, StateContext.Data["s"]);
			Assert.AreEqual("2", StateContext.Data["t"]);
			data["s"] = 3;
			data["t"] = "3";
			StateController.Navigate("t1", data);
			Assert.AreEqual(1, StateController.Crumbs[0].Data["s"]);
			Assert.IsNull(StateController.Crumbs[0].Data["t"]);
			Assert.AreEqual(2, StateController.Crumbs[1].Data["s"]);
			Assert.AreEqual("2", StateController.Crumbs[1].Data["t"]);
			Assert.AreEqual(3, StateContext.Data["s"]);
			Assert.AreEqual("3", StateContext.Data["t"]);
		}

		[TestMethod]
		public void NavigateDynamicDataNavigateTransitionTransitionTest()
		{
			NavigationData data = new NavigationData();
			data.Bag.s = 1;
			StateController.Navigate("d0", data);
			Assert.AreEqual(1, StateContext.Bag.s);
			data.Bag.s = "2";
			StateController.Navigate("t1", data);
			Assert.AreEqual(1, StateController.Crumbs[0].Bag.s);
			Assert.AreEqual("2", StateContext.Bag.s);
			data.Bag.s = '3';
			StateController.Navigate("t1", data);
			Assert.AreEqual(1, StateController.Crumbs[0].Data.Bag.s);
			Assert.AreEqual("2", StateController.Crumbs[1].Bag.s);
			Assert.AreEqual('3', StateContext.Data.Bag.s);
		}

		[TestMethod]
		public void ChangeDataNavigateTransitionTransitionTest()
		{
			NavigationData data = new NavigationData();
			data["s"] = 1;
			StateController.Navigate("d0", data);
			Assert.AreEqual(1, StateContext.Data["s"]);
			StateContext.Data["s"] = 11;
			Assert.IsNull(StateContext.Data["t"]);
			data.Bag.s = 2;
			data["t"] = "2";
			StateController.Navigate("t1", data);
			Assert.AreEqual(11, StateController.Crumbs[0]["s"]);
			Assert.IsNull(StateController.Crumbs[0]["t"]);
			Assert.AreEqual(2, StateContext.Data["s"]);
			Assert.AreEqual("2", StateContext.Data["t"]);
			StateContext.Data["t"] = "22";
			data["s"] = 3;
			data.Bag.t = "3";
			StateController.Navigate("t1", data);
			Assert.AreEqual(11, StateController.Crumbs[0].Data["s"]);
			Assert.IsNull(StateController.Crumbs[0].Bag.t);
			Assert.AreEqual(2, StateController.Crumbs[1].Data["s"]);
			Assert.AreEqual("22", StateController.Crumbs[1].Data["t"]);
			Assert.AreEqual(3, StateContext.Bag.s);
			Assert.AreEqual("3", StateContext.Data["t"]);
		}

		[TestMethod]
		public void ChangeCrumbDataNavigateBackTest()
		{
			NavigationData data = new NavigationData();
			data["s"] = "Hello";
			StateController.Navigate("d0", data);
			StateController.Navigate("t0");
			Crumb crumb = StateController.Crumbs[0];
			crumb.Data["s"] = "Changed";
			Assert.AreEqual("Changed", crumb.Data["s"]);
			Assert.AreEqual("Hello", StateController.Crumbs[0].Data["s"]);
			StateController.NavigateBack(1);
			Assert.AreEqual("Hello", StateContext.Data["s"]);
		}

		[TestMethod]
		[ExpectedException(typeof(UrlException))]
		public void NavigateHistoryInvalidDataTest()
		{
			StateController.Navigate("d0");
			NameValueCollection coll = new NameValueCollection(){
		        {"name","22_1"},
		        {"cs","VLijzPeq"}
		    };
			StateController.NavigateHistory(coll);
		}

		[TestMethod]
		public void NavigateHistoryIndividualDataTest()
		{
			StateController.Navigate("d2");
			StateController.Navigate("t0");
			string url = StateController.GetNavigationLink("t0", IndividualNavigationData);
			NameValueCollection coll = HttpUtility.ParseQueryString(url.Substring(url.IndexOf("?", StringComparison.Ordinal)));
			StateController.NavigateHistory(coll);
			int i = 0;
			foreach (NavigationDataItem item in StateContext.Data)
			{
				Assert.AreEqual(IndividualNavigationData[item.Key], item.Value);
				i++;
			}
			Assert.AreEqual(16, i);
		}

		[TestMethod]
		public void NavigateHistoryListDataTest()
		{
			StateController.Navigate("d2");
			StateController.Navigate("t0");
			string url = StateController.GetNavigationLink("t0", ListNavigationData);
			NameValueCollection coll = HttpUtility.ParseQueryString(url.Substring(url.IndexOf("?", StringComparison.Ordinal)));
			StateController.NavigateHistory(coll);
			int i = 0;
			foreach (NavigationDataItem item in (IEnumerable)StateContext.Data)
			{
				Assert.AreEqual(((IList)ListNavigationData[item.Key])[0], ((IList)item.Value)[0]);
				Assert.AreEqual(((IList)ListNavigationData[item.Key])[1], ((IList)item.Value)[1]);
				i++;
			}
			Assert.AreEqual(15, i);
		}

		[TestMethod]
		public void NavigateHistoryArrayListDataTest()
		{
			StateController.Navigate("d2");
			StateController.Navigate("t0");
			string url = StateController.GetNavigationLink("t0", ArrayListNavigationData);
			NameValueCollection coll = HttpUtility.ParseQueryString(url.Substring(url.IndexOf("?", StringComparison.Ordinal)));
			StateController.NavigateHistory(coll);
			int i = 0;
			foreach (NavigationDataItem item in StateContext.Data)
			{
				Assert.AreEqual(((IList)ArrayListNavigationData[item.Key])[0], ((IList)item.Value)[0]);
				Assert.AreEqual(((IList)ArrayListNavigationData[item.Key])[1], ((IList)item.Value)[1]);
				i++;
			}
			Assert.AreEqual(15, i);
		}

		[TestMethod]
		public void NavigateDefaultsTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			Assert.AreEqual("Hello", StateContext.Data["string"]);
			Assert.AreEqual(true, StateContext.Bag._bool);
			Assert.AreEqual(0, StateContext.Bag._int);
			Assert.AreEqual((short)1, StateContext.Data["short"]);
			Assert.AreEqual(2L, StateContext.Data["long"]);
			Assert.AreEqual(3F, StateContext.Data["float"]);
		}

		[TestMethod]
		public void NavigationDataDefaultsTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			NavigationData data = new NavigationData(true);
			data["string"] = null;
			data.Bag._int = "Hello";
			Assert.IsNull(data["string"]);
			Assert.AreEqual(true, data.Bag._bool);
			Assert.AreEqual("Hello", data.Bag._int);
			Assert.AreEqual((short)1, data["short"]);
			Assert.AreEqual(2L, data["long"]);
			Assert.AreEqual(3F, data["float"]);
		}

		[TestMethod]
		public void BlankNavigationDataDefaultsTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			NavigationData data = new NavigationData();
			Assert.IsNull(data["string"]);
			Assert.IsNull(data.Bag._bool);
			Assert.IsNull(data.Bag._int);
			Assert.IsNull(data["short"]);
			Assert.IsNull(data["long"]);
			Assert.IsNull(data["float"]);
		}

		[TestMethod]
		public void RemoveDefaultsTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			StateController.Navigate("t0");
			StateContext.Data["emptyString"] = null;
			StateContext.Data["double"] = null;
			StateContext.Data["decimal"] = null;
			StateContext.Data["DateTime"] = null;
			StateContext.Data["byte"] = null;
			StateContext.Data["char"] = null;
			Assert.AreEqual("", StateContext.Data["emptyString"]);
			Assert.AreEqual(4D, StateContext.Data["double"]);
			Assert.AreEqual(5m, StateContext.Data["decimal"]);
			Assert.AreEqual(new DateTime(1990, 3, 1, 12, 35, 47), StateContext.Bag.DateTime);
			Assert.AreEqual((byte)6, StateContext.Data["byte"]);
			Assert.AreEqual('7', StateContext.Data["char"]);
		}

		[TestMethod]
		public void EmptyStringDefaultsTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			StateController.Navigate("t0");
			StateContext.Data["emptyString"] = string.Empty;
			StateContext.Data["double"] = string.Empty;
			StateContext.Data["decimal"] = string.Empty;
			StateContext.Data["DateTime"] = string.Empty;
			StateContext.Data["byte"] = string.Empty;
			StateContext.Data["char"] = string.Empty;
			Assert.AreEqual("", StateContext.Data["emptyString"]);
			Assert.AreEqual(4D, StateContext.Data["double"]);
			Assert.AreEqual(5m, StateContext.Data["decimal"]);
			Assert.AreEqual(new DateTime(1990, 3, 1, 12, 35, 47), StateContext.Bag.DateTime);
			Assert.AreEqual((byte)6, StateContext.Data["byte"]);
			Assert.AreEqual('7', StateContext.Data["char"]);
		}

		[TestMethod]
		public void NavigateDataAndDefaultsTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			NavigationData data = new NavigationData(){
				{ "s" , 1 }, { "t" , ""}, {"double", string.Empty}
			};
			StateController.Navigate("t0", data);
			Assert.AreEqual(string.Empty, StateContext.Bag.emptyString);
			Assert.AreEqual(4D, StateContext.Data["double"]);
			Assert.AreEqual(5m, StateContext.Data["decimal"]);
			Assert.AreEqual(new DateTime(1990, 3, 1, 12, 35, 47), StateContext.Bag.DateTime);
			Assert.AreEqual((byte)6, StateContext.Data["byte"]);
			Assert.AreEqual('7', StateContext.Data["char"]);
			Assert.AreEqual(1, StateContext.Bag.s);
			Assert.IsNull(StateContext.Data["t"]);
		}

		[TestMethod]
		public void NavigateOverrideDefaultsTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			NavigationData data = new NavigationData(){
				{ "emptyString" , 2 }, { "double" , 1D }, { "decimal" , 5m}
			};
			StateController.Navigate("t0", data);
			Assert.AreEqual(2, StateContext.Bag.emptyString);
			Assert.AreEqual(1D, StateContext.Data["double"]);
			Assert.AreEqual(5m, StateContext.Data["decimal"]);
		}

		[TestMethod]
		public void OverrideDefaultsTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			StateController.Navigate("t0");
			StateContext.Data["emptyString"] = "Hello";
			StateContext.Data["double"] = 4D;
			StateContext.Bag.DateTime = new DateTime(2000, 4, 2);
			Assert.AreEqual("Hello", StateContext.Data["emptyString"]);
			Assert.AreEqual(4D, StateContext.Data["double"]);
			Assert.AreEqual(new DateTime(2000, 4, 2), StateContext.Data["DateTime"]);
			Assert.AreEqual('7', StateContext.Data["char"]);
		}

		[TestMethod]
		public void ClearDataAndDefaultsTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			NavigationData data = new NavigationData(){
				{ "s" , 1 }, { "t" , "2"}
			};
			StateController.Navigate("t0", data);
			StateContext.Data.Clear();
			Assert.AreEqual("", StateContext.Bag.emptyString);
			Assert.AreEqual(4D, StateContext.Data["double"]);
			Assert.AreEqual(5m, StateContext.Data["decimal"]);
			Assert.AreEqual(new DateTime(1990, 3, 1, 12, 35, 47), StateContext.Bag.DateTime);
			Assert.AreEqual((byte)6, StateContext.Data["byte"]);
			Assert.AreEqual('7', StateContext.Data["char"]);
			Assert.IsNull(StateContext.Bag.s);
			Assert.IsNull(StateContext.Data["t"]);
		}

		[TestMethod]
		public void NavigateBackDefaultsTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			StateController.Navigate("t0");
			StateController.NavigateBack(1);
			Assert.AreEqual("Hello", StateContext.Data["string"]);
			Assert.AreEqual(true, StateContext.Bag._bool);
			Assert.AreEqual(0, StateContext.Bag._int);
			Assert.AreEqual((short)1, StateContext.Data["short"]);
			Assert.AreEqual(2L, StateContext.Data["long"]);
			Assert.AreEqual(3F, StateContext.Data["float"]);
		}

		[TestMethod]
		public void NavigateBackDataAndDefaultsTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			NavigationData data = new NavigationData(){
				{ "s" , 1 }, { "t" , "2"}
			};
			StateController.Navigate("t0", data);
			StateController.Navigate("t0");
			StateController.Navigate("t0");
			StateController.NavigateBack(2);
			Assert.AreEqual(string.Empty, StateContext.Bag.emptyString);
			Assert.AreEqual(4D, StateContext.Data["double"]);
			Assert.AreEqual(5m, StateContext.Data["decimal"]);
			Assert.AreEqual(new DateTime(1990, 3, 1, 12, 35, 47), StateContext.Bag.DateTime);
			Assert.AreEqual((byte)6, StateContext.Data["byte"]);
			Assert.AreEqual('7', StateContext.Data["char"]);
			Assert.AreEqual(1, StateContext.Bag.s);
			Assert.AreEqual("2", StateContext.Data["t"]);
		}

		[TestMethod]
		public void NavigateBackOverrideDefaultsTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			NavigationData data = new NavigationData(){
				{ "emptyString", "World" }, { "double" , 1D }, { "decimal" , 5m}
			};
			StateController.Navigate("t0", data);
			StateController.Navigate("t0");
			StateController.NavigateBack(1);
			Assert.AreEqual("World", StateContext.Data["emptyString"]);
			Assert.AreEqual(1D, StateContext.Data["double"]);
			Assert.AreEqual(5m, StateContext.Data["decimal"]);
		}

		[TestMethod]
		public void CrumbDefaultsTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			StateController.Navigate("t0");
			StateController.Navigate("t0");
			Assert.IsNull(StateController.Crumbs[0].Data["string"]);
			Assert.IsNull(StateController.Crumbs[0].Data["long"]);
			Assert.AreEqual("Hello", StateController.Crumbs[1].Data["string"]);
			Assert.AreEqual(true, StateController.Crumbs[1].Data.Bag._bool);
			Assert.AreEqual(0, StateController.Crumbs[1].Bag._int);
			Assert.AreEqual((short)1, StateController.Crumbs[1].Data["short"]);
			Assert.AreEqual(2L, StateController.Crumbs[1].Data["long"]);
			Assert.AreEqual(3F, StateController.Crumbs[1].Data["float"]);
			Assert.AreEqual("", StateController.Crumbs[2]["emptyString"]);
			Assert.AreEqual(4D, StateController.Crumbs[2].Data["double"]);
			Assert.AreEqual(5m, StateController.Crumbs[2].Data["decimal"]);
			Assert.AreEqual(new DateTime(1990, 3, 1, 12, 35, 47), StateController.Crumbs[2].Bag.DateTime);
			Assert.AreEqual((byte)6, StateController.Crumbs[2].Data["byte"]);
			Assert.AreEqual('7', StateController.Crumbs[2].Data["char"]);
		}

		[TestMethod]
		public void CrumbDataAndDefaultsTest()
		{
			StateController.Navigate("d0");
			NavigationData data = new NavigationData(){
				{ "s" , 1 }, { "t" , "2"}
			};
			StateController.Navigate("t0", data);
			StateController.Navigate("t0");
			Assert.IsNull(StateController.Crumbs[0].Data["string"]);
			Assert.IsNull(StateController.Crumbs[0].Bag.s);
			Assert.AreEqual("Hello", StateController.Crumbs[1].Data["string"]);
			Assert.AreEqual(true, StateController.Crumbs[1].Data.Bag._bool);
			Assert.AreEqual(0, StateController.Crumbs[1].Bag._int);
			Assert.AreEqual((short)1, StateController.Crumbs[1].Data["short"]);
			Assert.AreEqual(2L, StateController.Crumbs[1].Data["long"]);
			Assert.AreEqual(3F, StateController.Crumbs[1].Data["float"]);
			Assert.AreEqual(1, StateController.Crumbs[1].Bag.s);
			Assert.AreEqual("2", StateController.Crumbs[1].Data["t"]);
		}

		[TestMethod]
		public void NavigateOverrideCrumbDefaultsTest()
		{
			StateController.Navigate("d0");
			NavigationData data = new NavigationData();
			data["string"] = "World";
			data.Bag._int = 0;
			StateController.Navigate("t0", data);
			StateController.Navigate("t0");
			Assert.AreEqual("World", StateController.Crumbs[1].Data["string"]);
			Assert.AreEqual(true, StateController.Crumbs[1].Data.Bag._bool);
			Assert.AreEqual(0, StateController.Crumbs[1].Bag._int);
			Assert.AreEqual((short)1, StateController.Crumbs[1].Data["short"]);
			Assert.AreEqual(2L, StateController.Crumbs[1].Data["long"]);
			Assert.AreEqual(3F, StateController.Crumbs[1].Data["float"]);
		}

		[TestMethod]
		public void OverrideCrumbDefaultsTest()
		{
			StateController.Navigate("d0");
			NavigationData data = new NavigationData();
			StateController.Navigate("t0", data);
			StateController.Navigate("t0");
			Crumb crumb = StateController.Crumbs[1];
			crumb.Data["string"] = "Hello";
			crumb.Data.Bag._int = 1;
			Assert.AreEqual("Hello", crumb.Data["string"]);
			Assert.AreEqual(1, crumb.Bag._int);
			Assert.AreEqual((short)1, crumb.Data["short"]);
		}

		[TestMethod]
		public void ClearCrumbDataAndDefaultsTest()
		{
			StateController.Navigate("d0");
			NavigationData data = new NavigationData(){
				{ "s" , 1 }, { "t" , "2"}
			};
			StateController.Navigate("t0", data);
			StateController.Navigate("t0");
			Crumb crumb = StateController.Crumbs[1];
			crumb.Data.Clear();
			Assert.AreEqual("Hello", crumb.Data["string"]);
			Assert.AreEqual(true, crumb.Data.Bag._bool);
			Assert.AreEqual(0, crumb.Bag._int);
			Assert.AreEqual((short)1, crumb.Data["short"]);
			Assert.AreEqual(2L, crumb.Data["long"]);
			Assert.AreEqual(3F, crumb.Data["float"]);
			Assert.IsNull(crumb.Bag.s);
			Assert.IsNull(crumb.Data["t"]);
		}

		[TestMethod]
		public void DefaultsViewStateTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			NavigationData data = new NavigationData();
			((IStateManager)data).LoadViewState(((IStateManager)StateContext.Data).SaveViewState());
			Assert.IsNull(data["string"]);
			Assert.IsNull(data.Bag._bool);
			Assert.IsNull(data.Bag._int);
			Assert.IsNull(data["short"]);
			Assert.IsNull(data["long"]);
			Assert.IsNull(data["float"]);
		}

		[TestMethod]
		public void NavigateOverrideDefaultsViewStateTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			NavigationData data = new NavigationData();
			data.Bag.DateTime = new DateTime(1990, 3, 1, 12, 35, 47);
			data.Add("double", "");
			data.Add("byte", '2');
			data.Add("char", (byte)0);
			StateController.Navigate("t0", data);
			data = new NavigationData();
			((IStateManager)data).LoadViewState(((IStateManager)StateContext.Data).SaveViewState());
			Assert.IsNull(data["decimal"]);
			Assert.IsNull(data["double"]);
			Assert.IsNull(data.Bag.DateTime);
			Assert.IsNull(data["emptyString"]);
			Assert.AreEqual('2', data["byte"]);
			Assert.AreEqual((byte)0, data["char"]);
		}

		[TestMethod]
		public void OverrideDefaultsViewStateTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			StateController.Navigate("t0");
			StateContext.Data["emptyString"] = 1;
			StateContext.Data["double"] = "World";
			StateContext.Bag.DateTime = 5D;
			StateContext.Data["char"] = '7';
			NavigationData data = new NavigationData();
			((IStateManager)data).LoadViewState(((IStateManager)StateContext.Data).SaveViewState());
			Assert.IsNull(data["decimal"]);
			Assert.IsNull(data["byte"]);
			Assert.IsNull(data["char"]);
			Assert.AreEqual(1, data.Bag.emptyString);
			Assert.AreEqual("World", data["double"]);
			Assert.AreEqual(5D, data.Bag.DateTime);
		}

		[TestMethod]
		public void LoadViewStateTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			StateController.Navigate("t0");
			StateContext.Data.Bag.emptyString = null;
			StateContext.Data["double"] = "World";
			StateContext.Bag.DateTime = 5D;
			StateContext.Data["char"] = null;
			StateContext.Data["t"] = "1";
			object viewState = ((IStateManager)StateContext.Data).SaveViewState();
			StateContext.Data.Clear();
			((IStateManager)StateContext.Data).LoadViewState(viewState);
			Assert.AreEqual(string.Empty, StateContext.Data["emptyString"]);
			Assert.AreEqual(5m, StateContext.Data["decimal"]);
			Assert.AreEqual((byte)6, StateContext.Data["byte"]);
			Assert.AreEqual('7', StateContext.Data["char"]);
			Assert.AreEqual("World", StateContext.Data["double"]);
			Assert.AreEqual(5D, StateContext.Data.Bag.DateTime);
			Assert.AreEqual("1", StateContext.Data.Bag.t);
		}

		[TestMethod]
		public void NavigateLinkDefaultsTest()
		{
			StateController.Navigate("d0");
			NavigationData data = new NavigationData();
			data["_bool"] = null;
			data["string"] = "Hello";
			data.Bag._int = 1D;
			data["short"] = string.Empty;
			data["float"] = "World";
			string link = StateController.GetNavigationLink("t0", data);
			Assert.AreEqual(-1, link.IndexOf("string"));
			Assert.AreEqual(-1, link.IndexOf("_bool"));
			Assert.AreEqual(-1, link.IndexOf("short"));
			Assert.AreEqual(-1, link.IndexOf("long"));
			Assert.AreNotEqual(-1, link.IndexOf("float"));
			Assert.AreNotEqual(-1, link.IndexOf("_int"));
		}

		[TestMethod]
		public void NavigateLinkContextDefaultsTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			StateController.Navigate("t0");
			StateContext.Data["emptyString"] = 1;
			StateContext.Data["double"] = 4D;
			StateContext.Bag.DateTime = 1D;
			StateContext.Data["byte"] = string.Empty;
			StateContext.Data["char"] = null;
			string link = StateController.GetNavigationLink("t0");
			Assert.AreEqual(-1, link.IndexOf("double"));
			Assert.AreEqual(-1, link.IndexOf("decimal"));
			Assert.AreEqual(-1, link.IndexOf("byte"));
			Assert.AreEqual(-1, link.IndexOf("char"));
			Assert.AreNotEqual(-1, link.IndexOf("emptyString"));
			Assert.AreNotEqual(-1, link.IndexOf("DateTime"));
		}

		[TestMethod]
		public void RefreshLinkDefaultsTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			StateContext.Data["_bool"] = null;
			StateContext.Data["string"] = "Hello";
			StateContext.Bag._int = 1D;
			StateContext.Data["short"] = string.Empty;
			StateContext.Data["float"] = "World";
			string link = StateController.GetRefreshLink(new NavigationData(true));
			Assert.AreEqual(-1, link.IndexOf("string"));
			Assert.AreEqual(-1, link.IndexOf("_bool"));
			Assert.AreEqual(-1, link.IndexOf("short"));
			Assert.AreEqual(-1, link.IndexOf("long"));
			Assert.AreNotEqual(-1, link.IndexOf("float"));
			Assert.AreNotEqual(-1, link.IndexOf("_int"));
		}

		[TestMethod]
		public void BackLinkDefaultsTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			StateContext.Data["_bool"] = null;
			StateContext.Data["string"] = "Hello";
			StateContext.Bag._int = 1D;
			StateContext.Data["short"] = string.Empty;
			StateContext.Data["float"] = "World";
			StateController.Navigate("t0");
			string link = StateController.GetNavigationBackLink(1);
			Assert.AreEqual(-1, link.IndexOf("string"));
			Assert.AreEqual(-1, link.IndexOf("_bool"));
			Assert.AreEqual(-1, link.IndexOf("short"));
			Assert.AreEqual(-1, link.IndexOf("long"));
			Assert.AreNotEqual(-1, link.IndexOf("float"));
			Assert.AreNotEqual(-1, link.IndexOf("_int"));
		}

		[TestMethod]
		public void CrumbLinkDefaultsTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			StateContext.Bag._int = null;
			StateContext.Data.Bag._bool = "";
			StateContext.Data["string"] = 4F;
			StateContext.Data["short"] = 1D;
			StateContext.Data["float"] = 3F;
			StateController.Navigate("t0");
			string link = StateController.Crumbs[1].NavigationLink;
			Assert.AreEqual(-1, link.IndexOf("_bool"));
			Assert.AreEqual(-1, link.IndexOf("_int"));
			Assert.AreEqual(-1, link.IndexOf("long"));
			Assert.AreEqual(-1, link.IndexOf("float"));
			Assert.AreNotEqual(-1, link.IndexOf("string"));
			Assert.AreNotEqual(-1, link.IndexOf("short"));
		}

		[TestMethod]
		public void NavigateLinkTest()
		{
			StateController.Navigate("d2");
			StateContext.Bag._int = 1;
			StateContext.Data["string"] = "Hello";
			string link = StateController.GetNavigationLink("t0");
			Assert.AreNotEqual(-1, link.IndexOf("c1"));
			Assert.AreNotEqual(-1, link.IndexOf("_int"));
			Assert.AreNotEqual(-1, link.IndexOf("string"));
		}

		[TestMethod]
		public void NavigateLinkWithoutTrailTest()
		{
			StateController.Navigate("d2");
			StateController.Navigate("t0");
			StateContext.Bag._int = 1;
			StateContext.Data["string"] = "Hello";
			string link = StateController.GetNavigationLink("t0");
			Assert.AreEqual(-1, link.IndexOf("c1"));
			Assert.AreEqual(-1, link.IndexOf("_int"));
			Assert.AreEqual(-1, link.IndexOf("string"));
		}

		[TestMethod]
		public void NavigateDefaultTypesTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			StateController.Navigate("t0");
			StateController.Navigate("t0");
			StateController.Navigate("t0", IndividualNavigationData);
			int i = 0;
			foreach (NavigationDataItem item in StateContext.Data)
			{
				Assert.AreEqual(IndividualNavigationData[item.Key], item.Value);
				i++;
			}
			Assert.AreEqual(16, i);
		}

		[TestMethod]
		public void NavigateLinkDefaultTypesStringTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			StateController.Navigate("t0");
			NavigationData data = new NavigationData()
			{
				{"s1","hello"},
				{"s2","world"}
			};
			string url = StateController.GetNavigationLink("t0", data);
			Assert.AreNotEqual(-1, url.IndexOf("s1=hello&"));
			Assert.AreNotEqual(-1, url.IndexOf("s2=world2_"));
		}

		[TestMethod]
		public void NavigateLinkDefaultTypesBoolTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			StateController.Navigate("t0");
			NavigationData data = new NavigationData()
			{
				{"b1",true},
				{"b2",false}
			};
			string url = StateController.GetNavigationLink("t0", data);
			Assert.AreNotEqual(-1, url.IndexOf("b1=True&"));
			Assert.AreNotEqual(-1, url.IndexOf("b2=False2_"));
		}

		[TestMethod]
		public void NavigateLinkDefaultTypesIntTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			StateController.Navigate("t0");
			NavigationData data = new NavigationData()
			{
				{"i1",0},
				{"i2",1}
			};
			string url = StateController.GetNavigationLink("t0", data);
			Assert.AreNotEqual(-1, url.IndexOf("i1=0&"));
			Assert.AreNotEqual(-1, url.IndexOf("i2=12_"));
		}

		[TestMethod]
		public void NavigateLinkDefaultTypesShortTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			StateController.Navigate("t0");
			NavigationData data = new NavigationData()
			{
				{"sh1",(short)0},
				{"sh2",(short)1}
			};
			string url = StateController.GetNavigationLink("t0", data);
			Assert.AreNotEqual(-1, url.IndexOf("sh1=0&"));
			Assert.AreNotEqual(-1, url.IndexOf("sh2=12_"));
		}

		[TestMethod]
		public void NavigateLinkDefaultTypesLongTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			StateController.Navigate("t0");
			NavigationData data = new NavigationData()
			{
				{"l2",(long)1},
				{"l1",(long)2}
			};
			string url = StateController.GetNavigationLink("t0", data);
			Assert.AreNotEqual(-1, url.IndexOf("l1=2&"));
			Assert.AreNotEqual(-1, url.IndexOf("l2=12_"));
		}

		[TestMethod]
		public void NavigateLinkDefaultTypesFloatTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			StateController.Navigate("t0");
			NavigationData data = new NavigationData()
			{
				{"f1",(float)3},
				{"f2",(float)4}
			};
			string url = StateController.GetNavigationLink("t0", data);
			Assert.AreNotEqual(-1, url.IndexOf("f1=3&"));
			Assert.AreNotEqual(-1, url.IndexOf("f2=42_"));
		}

		[TestMethod]
		public void NavigateLinkDefaultTypesDoubleTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			StateController.Navigate("t0");
			NavigationData data = new NavigationData()
			{
				{"d2",(double)1},
				{"d1",(double)0}
			};
			string url = StateController.GetNavigationLink("t0", data);
			Assert.AreNotEqual(-1, url.IndexOf("d1=0&"));
			Assert.AreNotEqual(-1, url.IndexOf("d2=12_"));
		}

		[TestMethod]
		public void NavigateLinkDefaultTypesDecimalTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			StateController.Navigate("t0");
			NavigationData data = new NavigationData()
			{
				{"de1",(decimal)5},
				{"de2",(decimal)5}
			};
			string url = StateController.GetNavigationLink("t0", data);
			Assert.AreNotEqual(-1, url.IndexOf("de1=5&"));
			Assert.AreNotEqual(-1, url.IndexOf("de2=52_"));
		}

		[TestMethod]
		public void NavigateLinkDefaultTypesDateTimeTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			StateController.Navigate("t0");
			NavigationData data = new NavigationData()
			{
				{"dt1",new DateTime(1990, 3, 1, 12, 35, 47)},
				{"dt2",new DateTime(1990, 3, 1, 12, 35, 47)}
			};
			string url = StateController.GetNavigationLink("t0", data);
			Assert.AreNotEqual(-1, url.IndexOf("dt1=" + HttpUtility.UrlEncode("03/01/1990 12:35:47") + "2_"));
			Assert.AreNotEqual(-1, url.IndexOf("dt2=" + HttpUtility.UrlEncode("03/01/1990 12:35:47") + "&"));
		}

		[TestMethod]
		public void NavigateLinkDefaultTypesTimeSpanTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			StateController.Navigate("t0");
			NavigationData data = new NavigationData()
			{
				{"t2",new TimeSpan(10, 5, 23)},
				{"t1",new TimeSpan(10, 5, 23)}
			};
			string url = StateController.GetNavigationLink("t0", data);
			Assert.AreNotEqual(-1, url.IndexOf("t1=" + HttpUtility.UrlEncode("10:05:23") + "&"));
			Assert.AreNotEqual(-1, url.IndexOf("t2=" + HttpUtility.UrlEncode("10:05:23") + "2_"));
		}

		[TestMethod]
		public void NavigateLinkDefaultTypesByteTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			StateController.Navigate("t0");
			NavigationData data = new NavigationData()
		    {
		        {"by1",(byte)0},
		        {"by2",(byte)0}
		    };
			string url = StateController.GetNavigationLink("t0", data);
			Assert.AreNotEqual(-1, url.IndexOf("by1=0&"));
			Assert.AreNotEqual(-1, url.IndexOf("by2=02_"));
		}

		[TestMethod]
		public void NavigateLinkDefaultTypesCharTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			StateController.Navigate("t0");
			NavigationData data = new NavigationData()
		    {
		        {"ch1",'0'},
		        {"ch2",'1'}
		    };
			string url = StateController.GetNavigationLink("t0", data);
			Assert.AreNotEqual(-1, url.IndexOf("ch1=0&"));
			Assert.AreNotEqual(-1, url.IndexOf("ch2=12_"));
		}

		[TestMethod]
		public void NavigateLinkDefaultTypesGuidTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			StateController.Navigate("t0");
			NavigationData data = new NavigationData()
		    {
		        {"g1",new Guid("01234567890123456789012345678901")},
		    };
			data.Bag.g2 = new Guid("01234567890123456789012345678901");
			string url = StateController.GetNavigationLink("t0", data);
			Assert.AreNotEqual(-1, url.IndexOf("g1=01234567-8901-2345-6789-012345678901&"));
			Assert.AreNotEqual(-1, url.IndexOf("g2=01234567-8901-2345-6789-0123456789012_"));
		}

		[TestMethod]
		public void NavigateBackLinkDefaultTypesTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			StateController.Navigate("t0");
			NavigationData data = new NavigationData()
			{
				{"s2","world"},
				{"i1",0},
				{"i2",1}
			};
			data.Bag.s1 = "hello";
			StateController.Navigate("t0", data);
			StateController.Navigate("t0");
			string url = StateController.GetNavigationBackLink(1);
			Assert.AreNotEqual(-1, url.IndexOf("s1=hello&"));
			Assert.AreNotEqual(-1, url.IndexOf("s2=world2_"));
			Assert.AreNotEqual(-1, url.IndexOf("i1=0&"));
			Assert.AreNotEqual(-1, url.IndexOf("i2=12_"));
		}

		[TestMethod]
		public void NavigateRefreshLinkDefaultTypesTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			StateController.Navigate("t0");
			NavigationData data = new NavigationData()
			{
				{"de1",(decimal)5},
				{"de2",(decimal)5},
		        {"ch1",'0'},
			};
			data.Bag.ch2 = '1';
			StateController.Navigate("t0", data);
			string url = StateController.GetRefreshLink(new NavigationData(true));
			Assert.AreNotEqual(-1, url.IndexOf("de1=5&"));
			Assert.AreNotEqual(-1, url.IndexOf("de2=52_"));
			Assert.AreNotEqual(-1, url.IndexOf("ch1=0&"));
			Assert.AreNotEqual(-1, url.IndexOf("ch2=12_"));
		}

		[TestMethod]
		public void NavigateBack2LinkDefaultTypesTest()
		{
			StateController.Navigate("d0");
			NavigationData data = new NavigationData()
			{
				{"_bool",1},
			};
			StateController.Navigate("t0", data);
			StateController.Navigate("t0");
			StateController.Navigate("t0");
			string url = StateController.GetNavigationBackLink(2);
			Assert.AreNotEqual(-1, url.IndexOf("_bool=1&"));
		}

		[TestMethod]
		public void NavigateOverrideDefaultTypesTest()
		{
			StateController.Navigate("d1");
			NavigationData data = new NavigationData();
			data["s1"] = true;
			data["b1"] = 0;
			data["i1"] = (short)1;
			data["sh1"] = (long)2;
			StateController.Navigate("t0", data);
			Assert.AreEqual(true, StateContext.Data["s1"]);
			Assert.AreEqual(0, StateContext.Data["b1"]);
			Assert.AreEqual((short)1, StateContext.Data["i1"]);
			Assert.AreEqual((long)2, StateContext.Data["sh1"]);
		}

		[TestMethod]
		public void NavigateRefreshOverrideDefaultTypesTest()
		{
			StateController.Navigate("d1");
			NavigationData data = new NavigationData();
			data["l1"] = (float)3;
			data["f1"] = (double)4;
			data["d1"] = (decimal)5;
			data["de1"] = new DateTime(1990, 3, 1, 12, 35, 47);
			StateController.Navigate("t0", data);
			StateController.Refresh(new NavigationData(true));
			Assert.AreEqual((float)3, StateContext.Data["l1"]);
			Assert.AreEqual((double)4, StateContext.Data["f1"]);
			Assert.AreEqual((decimal)5, StateContext.Data["d1"]);
			Assert.AreEqual(new DateTime(1990, 3, 1, 12, 35, 47), StateContext.Data["de1"]);
		}

		[TestMethod]
		public void NavigateBackOverrideDefaultTypesTest()
		{
			StateController.Navigate("d1");
			NavigationData data = new NavigationData();
			data["dt1"] = new TimeSpan(10, 5, 23);
			data["t1"] = (byte)7;
			data["by1"] = (char)8;
			data["ch1"] = new Guid("01234567890123456789012345678901");
			data["g1"] = "a";
			StateController.Navigate("t0", data);
			StateController.Navigate("t0");
			StateController.Navigate("t0");
			StateController.NavigateBack(2);
			Assert.AreEqual(new TimeSpan(10, 5, 23), StateContext.Data["dt1"]);
			Assert.AreEqual((byte)7, StateContext.Data["t1"]);
			Assert.AreEqual((char)8, StateContext.Data["by1"]);
			Assert.AreEqual(new Guid("01234567890123456789012345678901"), StateContext.Data["ch1"]);
			Assert.AreEqual("a", StateContext.Data["g1"]);
		}

		[TestMethod]
		public void ReservedUrlCharacterDefaultTypesTest()
		{
			NavigationData data = new NavigationData();
			data["*/()-_+~@:?><.;[]{}!£$%^&"] = (short)0;
			data["**=/()-_+~@:?><.;[]{}!£$%^&&"] = (short)1;
			StateController.Navigate("d1", data);
			string url = StateController.GetRefreshLink(new NavigationData(true));
			Assert.AreNotEqual(-1, url.IndexOf("=0&"));
			Assert.AreNotEqual(-1, url.IndexOf("=12_"));
			Assert.AreEqual((short)0, StateContext.Data["*/()-_+~@:?><.;[]{}!£$%^&"]);
			Assert.AreEqual((short)1, StateContext.Data["**=/()-_+~@:?><.;[]{}!£$%^&&"]);
			StateController.Navigate("t0");
			url = StateController.GetNavigationBackLink(1);
			Assert.AreNotEqual(-1, url.IndexOf("=0&"));
			Assert.AreNotEqual(-1, url.IndexOf("=12_"));
		}

		[TestMethod]
		public void SeparatorUrlCharacterDefaultTypesTest()
		{
			NavigationData data = new NavigationData();
			data["_0_1_2_3_4_5_"] = 10;
			data["__0_1_2_3_4_5_"] = 20;
			StateController.Navigate("d1", data);
			string url = StateController.GetRefreshLink(new NavigationData(true));
			Assert.AreNotEqual(-1, url.IndexOf("=10&"));
			Assert.AreNotEqual(-1, url.IndexOf("=202_"));
			Assert.AreEqual(10, StateContext.Data["_0_1_2_3_4_5_"]);
			Assert.AreEqual(20, StateContext.Data["__0_1_2_3_4_5_"]);
			StateController.Navigate("t0");
			StateController.Navigate("t0");
			StateController.Navigate("t0");
			StateController.NavigateBack(1);
			url = StateController.GetNavigationBackLink(2);
			Assert.AreNotEqual(-1, url.IndexOf("=10&"));
			Assert.AreNotEqual(-1, url.IndexOf("=202_"));
		}

		[TestMethod]
		public void NavigateLinkToDerivedTest()
		{
			StateController.Navigate("d0");
			NavigationData data = new NavigationData();
			data["st1"] = "Hello";
			data["bool"] = true;
			data["long"] = (long)1;
			data["st2"] = "World";
			string link = StateController.GetNavigationLink("t0", data);
			Assert.AreEqual(-1, link.IndexOf("st1"));
			Assert.AreEqual(-1, link.IndexOf("bool"));
			Assert.AreEqual(-1, link.IndexOf("long"));
			Assert.AreNotEqual(-1, link.IndexOf("st2"));
		}

		[TestMethod]
		public void NavigateLinkFromDerivedTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			StateContext.Data["st1"] = "Hello";
			StateContext.Data["bool"] = true;
			StateContext.Data["long"] = (long)1;
			StateContext.Data["st2"] = "World";
			StateController.Navigate("t0");
			string link = StateController.GetNavigationLink("t0");
			Assert.AreEqual(-1, link.IndexOf("st1"));
			Assert.AreEqual(-1, link.IndexOf("bool"));
			Assert.AreEqual(-1, link.IndexOf("long"));
			Assert.AreNotEqual(-1, link.IndexOf("st2"));
		}

		[TestMethod]
		public void NavigateBackLinkDerivedTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			StateContext.Bag.st1 = "Hello";
			StateContext.Data["bool"] = true;
			StateContext.Data["long"] = (long)1;
			StateContext.Bag.st2 = "World";
			StateController.Navigate("t0");
			string link = StateController.GetNavigationBackLink(1);
			Assert.AreEqual(-1, link.IndexOf("st1"));
			Assert.AreEqual(-1, link.IndexOf("bool"));
			Assert.AreEqual(-1, link.IndexOf("long"));
			Assert.AreNotEqual(-1, link.IndexOf("st2"));
		}

		[TestMethod]
		public void RefreshLinkDerivedTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			NavigationData data = new NavigationData();
			data["st1"] = "Hello";
			data["bool"] = true;
			data["long"] = (long)1;
			data["st2"] = "World";
			string link = StateController.GetRefreshLink(data);
			Assert.AreEqual(-1, link.IndexOf("st1"));
			Assert.AreEqual(-1, link.IndexOf("bool"));
			Assert.AreEqual(-1, link.IndexOf("long"));
			Assert.AreNotEqual(-1, link.IndexOf("st2"));
		}

		[TestMethod]
		public void NavigateDerivedTest()
		{
			StateController.Navigate("d0");
			NavigationData data = new NavigationData();
			data.Bag.st1 = "Hello";
			data["bool"] = true;
			data["st2"] = "World";
			StateController.Navigate("t0", data);
			Assert.IsNull(StateContext.Data["st1"]);
			Assert.IsNull(StateContext.Data["bool"]);
			Assert.AreEqual("World", StateContext.Bag.st2);
		}

		[TestMethod]
		public void NavigateDerivedDefaultTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			NavigationData data = new NavigationData();
			data["string"] = "Hello";
			data["decimal"] = "World";
			StateController.Navigate("t0", data);
			Assert.IsNull(StateContext.Data["string"]);
			Assert.AreEqual(5m, StateContext.Data["decimal"]);
		}

		[TestMethod]
		public void NavigateBackDerivedTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			StateContext.Bag.st1 = "Hello";
			StateContext.Data["bool"] = true;
			StateContext.Data["st2"] = "World";
			StateController.Navigate("t0");
			StateController.NavigateBack(1);
			Assert.IsNull(StateContext.Data["st1"]);
			Assert.IsNull(StateContext.Data["bool"]);
			Assert.AreEqual("World", StateContext.Bag.st2);
		}

		[TestMethod]
		public void NavigateBackDerivedDefaultTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			StateController.Navigate("t0");
			StateContext.Data["string"] = "Hello";
			StateContext.Data["decimal"] = "World";
			StateController.Navigate("t0");
			StateController.NavigateBack(1);
			Assert.IsNull(StateContext.Data["string"]);
			Assert.AreEqual(5m, StateContext.Data["decimal"]);
		}

		[TestMethod]
		public void NavigateDerivedNonConvertibleTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			StateController.Navigate("t0");
			NavigationData data = new NavigationData();
			data["string"] = new Exception();
			data["decimal"] = new Exception();
			StateController.Refresh(data);
			Assert.IsNull(StateContext.Data["string"]);
			Assert.AreEqual(5m, StateContext.Data["decimal"]);
		}

		[TestMethod]
		public void ParseNavigationDataExpressionCurrentDataTest()
		{
			StateController.Navigate("d0");
			NavigationData data = new NavigationData();
			data.Bag.z = "Hello";
			StateController.Navigate("t0", data);
			data = StateInfoConfig.ParseNavigationDataExpression("x=y,z,a?int=1", null, true);
			Assert.AreEqual("y", data.Bag.x);
			Assert.AreEqual("Hello", data.Bag.z);
			Assert.AreEqual(1, data.Bag.a);
		}

		[TestMethod]
		public void ParseNavigationDataExpressionNullCurrentDataTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			NavigationData data = StateInfoConfig.ParseNavigationDataExpression("x=y,z", null, true);
			Assert.AreEqual("y", data.Bag.x);
			Assert.IsNull(data.Bag.z);
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void ParseNavigationDataExpressionInvalidEqualCurrentDataTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			NavigationData data = StateInfoConfig.ParseNavigationDataExpression("x=y,z==", null, true);
		}

		[TestMethod]
		public void ParseNavigationDataExpressionCurrentDataDefaultsTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0");
			NavigationData data = StateInfoConfig.ParseNavigationDataExpression("_bool , x=y  , string ", null, true);
			Assert.AreEqual(true, data.Bag._bool);
			Assert.AreEqual("y", data.Bag.x);
			Assert.AreEqual("Hello", data["string"]);
		}

		[TestMethod]
		public void NavigateRefreshCurrentDataTest()
		{
			StateController.Navigate("d0");
			NavigationData data = new NavigationData();
			data["s"] = "Hello";
			data.Bag.i = 1;
			data["c"] = '1';
			StateController.Navigate("t0", data);
			List<string> currentDataKeys = new List<string>();
			currentDataKeys.Add("s");
			currentDataKeys.Add("c");
			StateController.Refresh(new NavigationData(currentDataKeys));
			Assert.AreEqual("Hello", StateContext.Data["s"]);
			Assert.AreEqual('1', StateContext.Bag.c);
			Assert.IsNull(StateContext.Data["i"]);
		}

		[TestMethod]
		public void NavigateCurrentDataDefaultsTest()
		{
			NavigationData data = new NavigationData();
			data["emptyString"] = "Hello";
			data["double"] = 1D;
			data["char"] = '6';
			data["DateTime"] = new DateTime(2000, 1, 3);
			StateController.Navigate("d0", data);
			StateController.Navigate("t0", new NavigationData(true));
			List<string> currentDataKeys = new List<string>();
			currentDataKeys.Add("double");
			currentDataKeys.Add("char");
			StateController.Navigate("t0", new NavigationData(currentDataKeys));
			Assert.AreEqual("", StateContext.Bag.emptyString);
			Assert.AreEqual(1D, StateContext.Data["double"]);
			Assert.AreEqual('6', StateContext.Data["char"]);
			Assert.AreEqual(new DateTime(1990, 3, 1, 12, 35, 47), StateContext.Bag.DateTime);
		}

		[TestMethod]
		public void NavigateCurrentDataDerivedDefaultsTest()
		{
			StateController.Navigate("d0");
			StateController.Navigate("t0", new NavigationData() { { "decimal", 3m } });
			List<string> currentDataKeys = new List<string>() { "decimal" };
			StateController.Navigate("t0", new NavigationData(currentDataKeys));
			Assert.AreEqual(5m, StateContext.Data["decimal"]);
		}

		[TestMethod]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void EmptyNavigationDataType()
		{
			ConfigurationManager.GetSection("Navigation/EmptyNavigationDataType");
		}

		[TestMethod]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void InvalidNavigationDataType()
		{
			ConfigurationManager.GetSection("Navigation/InvalidNavigationDataType");
		}

		[TestMethod]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void InvalidConversionFrom()
		{
			ConfigurationManager.GetSection("Navigation/InvalidConversionFrom");
		}

		[TestMethod]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void InvalidConversionTo()
		{
			ConfigurationManager.GetSection("Navigation/InvalidConversionTo");
		}

		[TestMethod]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void InvalidNavigationDataConverter()
		{
			ConfigurationManager.GetSection("Navigation/InvalidNavigationDataConverter");
		}

		[TestMethod]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void InvalidNavigationDataConverterType()
		{
			ConfigurationManager.GetSection("Navigation/InvalidNavigationDataConverterType");
		}

		[TestMethod]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void InvalidConversionFromConverter()
		{
			ConfigurationManager.GetSection("Navigation/InvalidConversionFromConverter");
		}

		[TestMethod]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void InvalidConversionToConverter()
		{
			ConfigurationManager.GetSection("Navigation/InvalidConversionToConverter");
		}
	}
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Systran.ResourcesClientLib.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Systran.ResourcesClientLib.Client;
using Systran.ResourcesClientLib.Model;
using System.IO;

namespace Systran.ResourcesClientLib.Api.Tests
{
    [TestClass()]
    public class DictionaryApiTests
    {
        private static ApiClient client;
        private static DictionaryApi dictionaryApi;
        private static String dictionaryId;


        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            client = new ApiClient("https://platform.systran.net:8904");
            Configuration.apiClient = client;
            if (!File.Exists("../../apiKey.txt"))
                throw new Exception("To properly run the tests, please add an apiKey.txt file containing your api key in the SystranClientResourcesApiLibTests folder or edit the test file with your key");
            Dictionary<String, String> keys = new Dictionary<String, String>();
            string key;
            using (StreamReader streamReader = new StreamReader("../../apiKey.txt", Encoding.UTF8))
            {
                key = streamReader.ReadToEnd();
            }
            keys.Add("key", key);
            if (keys.Count == 0)
                throw new Exception("No api key found, please check your apiKey.txt file");
            Configuration.apiKey = keys;
            dictionaryApi = new DictionaryApi(Configuration.apiClient);


        }

        [TestMethod()]
        public void GetBasePathTest()
        {
            Assert.IsInstanceOfType(dictionaryApi.apiClient.basePath, typeof(string));
        }

        [TestMethod()]
        public void ResourcesDictionaryAddPostTest()
        {
            DictionaryAddResponse dictionaryAddResponse = new DictionaryAddResponse();
            DictionaryAddBody dictionaryAddBody = new DictionaryAddBody();
            DictionaryAddInput dictionaryAddInput = new DictionaryAddInput();
            dictionaryAddInput.SourceLang = "en";
            dictionaryAddInput.TargetLangs = "fr";
            dictionaryAddInput.Name = "testCsharpClient";
            dictionaryAddInput.Type = "UD";
            dictionaryAddInput.Comments = "This dictionary has been created for csharp client testing purposes";

            dictionaryAddBody.Dictionary = dictionaryAddInput;
            dictionaryAddResponse = dictionaryApi.ResourcesDictionaryAddPost(dictionaryAddBody);
            Assert.IsNotNull(dictionaryAddResponse.Added);
            if (dictionaryAddResponse != null)
            {
                dictionaryId = dictionaryAddResponse.Added.Id;
            }

        }


        [TestMethod()]
        public void ResourcesDictionaryEntryAddPostTest()
        {
            EntryAddBody entryAddBody = new EntryAddBody();
            EntryAddInput entryAddInput = new EntryAddInput();
            entryAddInput.SourceLang = "en";
            entryAddInput.Source = "test";
            entryAddInput.TargetLang = "fr";
            entryAddInput.Target = "test";
            entryAddInput.Type = "";
            entryAddInput.SourcePos = "";
            entryAddInput.TargetPos = "";
            entryAddInput.Priority = "";
            entryAddBody.Entry = entryAddInput;
            EntryAddResponse entryAddResponse = new EntryAddResponse();
            entryAddResponse = dictionaryApi.ResourcesDictionaryEntryAddPost(dictionaryId, entryAddBody);
            Assert.IsNotNull(entryAddResponse);
        }

        [TestMethod()]
        public void ResourcesDictionaryEntryListPostTest()
        {
            EntriesListFilters entriesListFilters = new EntriesListFilters();
            EntriesListResponse entriesListResponse = new EntriesListResponse();
            entriesListResponse = dictionaryApi.ResourcesDictionaryEntryListPost(dictionaryId, entriesListFilters);
            Assert.IsNotNull(entriesListResponse.TotalNoLimit);
        }

        [TestMethod()]
        public void ResourcesDictionaryEntryUpdatePostTest()
        {

            EntriesListFilters entriesListFilters = new EntriesListFilters();
            EntriesListResponse entriesListResponse = new EntriesListResponse();
            entriesListResponse = dictionaryApi.ResourcesDictionaryEntryListPost(dictionaryId, entriesListFilters);

            EntryUpdateBody entryUpdateBody = new EntryUpdateBody();
            EntryUpdateInput entryUpdateInput = new EntryUpdateInput();
            EntryUpdateResponse entryUpdateResponse = new EntryUpdateResponse();
            entryUpdateBody.SourceId = entriesListResponse.Entries[0].SourceId;
            entryUpdateBody.TargetId = entriesListResponse.Entries[0].TargetId;

            entryUpdateInput.Priority = "";
            entryUpdateInput.Type = "";
            entryUpdateInput.SourcePos = "";
            entryUpdateInput.TargetPos = "";
            entryUpdateInput.Source = "example";
            entryUpdateInput.Target = "exemple";
            entryUpdateInput.TargetLang = "fr";
            entryUpdateInput.SourceLang = "en";
            entryUpdateBody.Update = entryUpdateInput;


            entryUpdateResponse = dictionaryApi.ResourcesDictionaryEntryUpdatePost(dictionaryId, entryUpdateBody);
            Assert.IsNotNull(entryUpdateResponse.TargetId);
        }

        [TestMethod()]
        public void ResourcesDictionaryListPostTest()
        {
            DictionariesListFilters dictionariesListFilters = new DictionariesListFilters();
            DictionariesListResponse dictionariesListResponse = new DictionariesListResponse();
            dictionariesListResponse = dictionaryApi.ResourcesDictionaryListPost(dictionariesListFilters);
            Assert.IsNotNull(dictionariesListResponse.Dictionaries);
        }

        [TestMethod()]
        public void ResourcesDictionaryLookupGetTest()
        {
            LookupResponse lookupResponse = new LookupResponse();
            List<string> inputs = new List<string>();
            inputs.Add("example");
            lookupResponse = dictionaryApi.ResourcesDictionaryLookupGet("en", "fr", inputs, null, null);
            Assert.IsNotNull(lookupResponse.Outputs);
        }

        [TestMethod()]
        public void ResourcesDictionaryLookupSupportedLanguagesGetTest()
        {
            LookupSupportedLanguageResponse lookupSupportedLanguageResponse = new LookupSupportedLanguageResponse();
            lookupSupportedLanguageResponse = dictionaryApi.ResourcesDictionaryLookupSupportedLanguagesGet(null, null, null);
            Assert.IsNotNull(lookupSupportedLanguageResponse.LanguagePairs);
        }
        
        [TestMethod()]
        public void ResourcesDictionarySupportedLanguagesGetTest()
        {
            SupportedLanguagesResponse supportedLanguagesResponse = new SupportedLanguagesResponse();
            supportedLanguagesResponse = dictionaryApi.ResourcesDictionarySupportedLanguagesGet();
            Assert.IsNotNull(supportedLanguagesResponse.Languages);
        }

        [TestMethod()]
        public void ResourcesDictionaryUpdatePostTest()
        {
            EntriesListResponse entriesListResponse = new EntriesListResponse();


            DictionaryUpdateBody dictionaryUpdateBody = new DictionaryUpdateBody();
            DictionaryUpdateInput dictionaryUpdateInput = new DictionaryUpdateInput();
            dictionaryUpdateInput.Comments = "This dictionary has been created and updated for csharp client testing purposes";
            dictionaryUpdateBody.Dictionary = dictionaryUpdateInput;

            DictionaryUpdateResponse dictionaryUpdateResponse = new DictionaryUpdateResponse();
            dictionaryUpdateResponse = dictionaryApi.ResourcesDictionaryUpdatePost(dictionaryId, dictionaryUpdateBody);
            Assert.IsNotNull(dictionaryUpdateResponse.Updated);
        }

        [TestMethod()]
        public void ResourcesDictionaryEntryDeletePostTest()
        {

            EntriesListResponse entriesListResponse = new EntriesListResponse();
            EntriesListFilters entriesListFilters = new EntriesListFilters();
            entriesListResponse = dictionaryApi.ResourcesDictionaryEntryListPost(dictionaryId, entriesListFilters);


            EntryDeleteBody entryDeleteBody = new EntryDeleteBody();
            EntryDeleteInput entryDeleteInput = new EntryDeleteInput();
            entryDeleteInput.SourceId = entriesListResponse.Entries[0].SourceId;
            entryDeleteInput.TargetId = entriesListResponse.Entries[0].TargetId;
            entryDeleteBody.Entry = entryDeleteInput;
            EntryDeleteResponse entryDeleteResponse = new EntryDeleteResponse();
            entryDeleteResponse = dictionaryApi.ResourcesDictionaryEntryDeletePost(dictionaryId, entryDeleteBody);
            Assert.IsNotNull(entryDeleteResponse.Status);
        }


        [TestMethod()]
        public void ResourcesDictionaryDeletePostTest()
        {
            dictionaryApi.ResourcesDictionaryDeletePost(dictionaryId);
        }

    }
}
using VersionChecker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace VersionChecker.Pages
{
    public class IndexModel : PageModel
    {
        private const string ERROR = "Please enter a valid version format.";

        // Regex assumes that the given example "2.12.4.0.6" is the longest accepted version
        // and there are not more than 2 digits starting at minor and below
        // Allowed: ###?.##?.##?.##?.##
        [RegularExpression(@"(\d{1,3}(\.\d{1,2}){0,4})", ErrorMessage = ERROR)]
        [BindProperty(SupportsGet = true)]
        public string? VersionSearch { get; set; }

        public List<Software> Results { get; set; } = new List<Software>();

        public void OnGet()
        {
            // Validate user input
            if (VersionSearch is null || !ModelState.IsValid)
                return;

            Results.Clear();

            // Compare entered version against static list
            foreach (var software in SoftwareManager.GetAllSoftware())
            {
                // Catch any other weird input the user may have entered
                try
                {
                    if (VersionCompare(VersionSearch, software.Version) == -1)
                        Results.Add(software);
                }
                catch
                {
                    ModelState.AddModelError("VersionSearch", ERROR);
                }
            }
        }

        private static int VersionCompare(string version1, string version2)
        {
            // Split each version into the separate numerical parts
            var splitVersion1 = version1.Split('.').Select(Int32.Parse).ToList();
            var splitVersion2 = version2.Split('.').Select(Int32.Parse).ToList();

            // Account for if one has more subcomponents (ex: "2" vs "2.5")
            var difference = Math.Abs(splitVersion1.Count - splitVersion2.Count);
            if (difference != 0)
            {
                if (splitVersion1.Count < splitVersion2.Count)
                {
                    for (int i = 0; i < difference; i++)
                    {
                        splitVersion1.Add(0);
                    }
                }
                else
                {
                    for (int i = 0; i < difference; i++)
                    {
                        splitVersion2.Add(0);
                    }
                }
            }

            // Version comparisions
            for (int i = 0; i < splitVersion1.Count; i++)
            {
                // If any v1 section is less than, return -1
                if (splitVersion1[i] < splitVersion2[i])
                    return -1;


                // If any v1 section is greater than, return 1
                if (splitVersion1[i] > splitVersion2[i])
                    return 1;
            }

            // If it got here, then all parts must be equal
            return 0;
        }
    }
}
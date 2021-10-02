using PM.Data.Context;
using PM.Data.Entity;
using PM.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PM.Service.Services
{
    public class PermitUsageService : IPermitUsageService
    {
        PermitManagementContext _dbContext = new PermitManagementContext();

        void IPermitUsageService.AddPermitUsage(PermitUsage permitUsage)
        {
            _dbContext.PermitUsages.Add(permitUsage);
            Save();
        }

        List<PermitUsage> IPermitUsageService.GetPermitUsages()
        {
            return _dbContext.PermitUsages.ToList();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }


        public void PreparePermit()
        {

            var usageDays = GetUsagesDays();
            var members = GetMembers();
            var titleTypes = members.GroupBy(x => x.TitleTypeId).Select(t => t.Key);

            foreach (var titleTypeId in titleTypes)
            {
                var membersByTitleTypes = GetMembersByTitleTypeId(members, titleTypeId);
                foreach (var member in membersByTitleTypes)
                {
                    AddUsageDayAutomatic(member, usageDays);
                }
            }

        }

        private void AddUsageDayAutomatic(Member member, List<UsagesDay> usagesDays)
        {
            var startDate = new DateTime(2021, 04, 01);
            var endDate = new DateTime(2021, 8, 01);

            var leavingCounter = 0;
            for (DateTime date = startDate; endDate.CompareTo(date) > 0; date = date.AddDays(1.0))
            {
                if (usagesDays.Any(x => x.TitleTypeId == member.TitleTypeId && x.UsageDay == date))
                {
                    leavingCounter = 0;
                }
                else if (IsWorkDay(date.DayOfWeek) && !usagesDays.Any(x => x.IsBlocked && x.UsageDay == date))
                {
                    leavingCounter++;

                }

                if (leavingCounter == member.LeavingRight)
                {
                    for (int i = 0; i < leavingCounter; i++)
                    {
                        var leavingDate = date.AddDays(-i);
                        if (IsWorkDay(leavingDate.DayOfWeek) && !usagesDays.Any(x => x.IsBlocked && x.UsageDay == leavingDate))
                        {
                            usagesDays.Add(new UsagesDay { IsSaved = false, TitleTypeId = member.TitleTypeId, UserId = member.Id, UsageDay = leavingDate });
                        }
                        else
                        {
                            leavingCounter++;
                        }
                    }
                    break;
                }
            }
        }

        private List<Member> GetMembersByTitleTypeId(List<Member> members, int titleTypeId)
        {
            return members.Where(x => x.TitleTypeId == titleTypeId).OrderBy(x => x.LeavingRight).ToList();
        }

        private List<Member> GetMembers()
        {
            //var members = new List<Member>();
            //members.Add(new Member { FirstName = "Volkan", LastName = "Uzun", LeavingRight = 5, TitleTypeId = 1, Id = 1 });
            //members.Add(new Member { FirstName = "İsmail", LastName = "Türüt", LeavingRight = 3, TitleTypeId = 2, Id = 3 });
            //members.Add(new Member { FirstName = "Osman", LastName = "Uzun", LeavingRight = 8, TitleTypeId = 1, Id = 2 });
            //return members;
            return _dbContext.Members.ToList();
        }

        private List<UsagesDay> GetUsagesDays()
        {
            var usageDays = new List<UsagesDay>();

            var permitUsage = _dbContext.PermitUsages.ToList();



            foreach (var item in permitUsage)
            {
                var disableDates = (item.LeavingEndDate - item.LeavingStartDate).Days;

                //var disableDates = (permitUsage.Select(x => x.LeavingEndDate).FirstOrDefault() - permitUsage.Select(q => q.LeavingStartDate).FirstOrDefault()).Days;

                for (int i = 0; i <= disableDates; i++)
                {

                    usageDays.Add(new UsagesDay { TitleTypeId = item.TitleTypeId, UsageDay = item.LeavingStartDate.AddDays(i) });
                }
            }

            //usageDays.Add(new UsagesDay { TitleTypeId = 1, UsageDay = new DateTime(2021, 4, 5) });
            //usageDays.Add(new UsagesDay { TitleTypeId = 1, UsageDay = new DateTime(2021, 4, 6) });
            //usageDays.Add(new UsagesDay { TitleTypeId = 1, UsageDay = new DateTime(2021, 4, 7) });
            //usageDays.Add(new UsagesDay { TitleTypeId = 1, UsageDay = new DateTime(2021, 4, 8) });


            var publicHolidays = GetPublicHolidays();
            AddPublicHolidaysToUsagesDays(usageDays, publicHolidays);
            return usageDays;
        }

        private void AddPublicHolidaysToUsagesDays(List<UsagesDay> usagesDays, List<PublicHoliday> publicHolidays)
        {
            foreach (var item in publicHolidays)
            {
                usagesDays.Add(new UsagesDay { IsSaved = true, UsageDay = item.StartDate, IsBlocked = true });
            }
        }

        private bool IsWorkDay(DayOfWeek dayOfWeek)
        {
            return (dayOfWeek >= DayOfWeek.Monday) && (dayOfWeek <= DayOfWeek.Friday);
        }

        private List<PublicHoliday> GetPublicHolidays()
        {
            //var publicHolidays = new List<PublicHoliday>();
            //publicHolidays.Add(new PublicHoliday { Day = 1, Description = "test1 public", StartDate = new DateTime(2021, 4, 13) });
            //return publicHolidays;
            return _dbContext.PublicHolidays.ToList();
        }
    }
}

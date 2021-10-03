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

        public void AddPermitUsage(PermitUsage permitUsage)
        {
            _dbContext.PermitUsages.Add(permitUsage);
            Save();
        }
        public void AddPermitUsage(List<PermitUsage> permitUsages)
        {
            _dbContext.PermitUsages.AddRange(permitUsages);
            Save();
        }

        public List<PermitUsage> GetPermitUsages()
        {
            return _dbContext.PermitUsages.ToList();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void DistributeLeaves()
        {
            var usagesDates = PreparePermit();
            var userList = usagesDates.GroupBy(x => x.UserId).Select(x => x.Key);
            var permitUsages = new List<PermitUsage>();

            foreach (var userId in userList)
            {
                var userPermits = usagesDates.Where(x => x.UserId == userId).OrderBy(x => x.UsageDay);
                var titleTypeId = userPermits.First().TitleTypeId;
                var leavingStartDate = userPermits.First().UsageDay;
                var leavingEndDate = userPermits.Last().UsageDay;

                permitUsages.Add(new PermitUsage { LeavingStartDate = leavingStartDate, LeavingEndDate = leavingEndDate, MemberId = userId, TitleTypeId = titleTypeId });
            }

            AddPermitUsage(permitUsages);
            SetUsersLeavingDates(0);
        }

        private List<UsagesDay> PreparePermit()
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
            return usageDays.Where(x => !x.IsSaved).ToList();
        }


        private int AddUsageDayAutomatic(Member member, List<UsagesDay> usagesDays)
        {
            var startDate = new DateTime(2021, 04, 01);
            var endDate = new DateTime(2021, 8, 31);
            var publicHolidays = GetPublicHolidays();

            var leavingCounter = 0;
            if (member.LeavingRight != 0)
            {

                for (DateTime date = startDate; endDate.CompareTo(date) > 0; date = date.AddDays(1.0))
                {

                    if (usagesDays.Any(x => x.TitleTypeId == member.TitleTypeId && x.UsageDay == date))
                    {
                        leavingCounter = 0;
                    }

                    else if (IsWorkDay(date.DayOfWeek) && !usagesDays.Any(x => x.IsBlocked && x.UsageDay == date && x.TitleTypeId == member.TitleTypeId) && !publicHolidays.Contains(date))
                    {
                        leavingCounter++;
                    }

                    if (leavingCounter == member.LeavingRight)
                    {
                        for (int i = 0; i < leavingCounter; i++)
                        {
                            var leavingDate = date.AddDays(-i);
                            if (IsWorkDay(leavingDate.DayOfWeek) && !usagesDays.Any(x => x.IsBlocked && x.UsageDay == leavingDate && x.TitleTypeId == member.TitleTypeId) && !publicHolidays.Contains(date))
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

            return leavingCounter;
        }

        private List<Member> GetMembersByTitleTypeId(List<Member> members, int titleTypeId)
        {
            return members.Where(x => x.TitleTypeId == titleTypeId).OrderBy(x => x.LeavingRight).ToList();
        }

        private List<Member> GetMembers()
        {
            return _dbContext.Members.ToList();
        }

        private List<UsagesDay> GetUsagesDays()
        {
            var usageDays = new List<UsagesDay>();

            var permitUsage = _dbContext.PermitUsages.ToList();



            foreach (var item in permitUsage)
            {
                var disableDates = (item.LeavingEndDate - item.LeavingStartDate).Days;

                for (int i = 0; i <= disableDates; i++)
                {
                    usageDays.Add(new UsagesDay { TitleTypeId = item.TitleTypeId, UsageDay = item.LeavingStartDate.AddDays(i), IsSaved = true, IsBlocked = true, UserId = item.MemberId });
                }
            }

            return usageDays;
        }

        private bool IsWorkDay(DayOfWeek dayOfWeek)
        {
            return (dayOfWeek >= DayOfWeek.Monday) && (dayOfWeek <= DayOfWeek.Friday);
        }

        private DateTime[] GetPublicHolidays()
        {
            return _dbContext.PublicHolidays.Select(x => x.StartDate).ToArray();
        }
        private List<TitleType> GetTitles()
        {
            return _dbContext.TitleTypes.ToList();
        }

        public List<UsageLeavesViewModel> GetCurretPermits()
        {
            List<UsageLeavesViewModel> usageLeaves = new List<UsageLeavesViewModel>();

            var permitUsages = GetPermitUsages();
            var members = GetMembers();
            var titles = GetTitles();
            foreach (var permitUsage in permitUsages)
            {
                var member = members.First(x => x.Id == permitUsage.MemberId);
                var title = titles.First(x => x.Id == permitUsage.TitleTypeId);
                usageLeaves.Add(new UsageLeavesViewModel
                {
                    Name = member.FirstName,
                    LastName = member.LastName,
                    Title = title.Title,
                    LeavingEndDate = permitUsage.LeavingEndDate,
                    LeavingStartDate = permitUsage.LeavingStartDate
                }); ;
            }
            return usageLeaves;
        }

        public void SetUsersLeavingDates(int usagesDates)
        {
            var members = GetMembers();
            foreach (var member in members)
            {
                member.LeavingRight = usagesDates;
                Save();
            }
        }
    }
}

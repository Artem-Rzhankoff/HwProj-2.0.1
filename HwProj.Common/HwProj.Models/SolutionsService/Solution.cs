﻿using HwProj.Repositories;
using System;

namespace HwProj.Models.SolutionsService
{
    public class Solution : IEntity<long>
    {
        public long Id { get; set; }

        public string GithubUrl { get; set; }
        
        public string Comment { get; set; }

        public SolutionState State { get; set; }

        public int Rating { get; set; }

        public string StudentId { get; set; }

        public string? LecturerId { get; set; }

        public long? GroupId { get; set; }

        public long TaskId { get; set; }

        public DateTime PublicationDate { get; set; }
        
        public DateTime? RatingDate { get; set; }

        public string LecturerComment { get; set; }
    }
}

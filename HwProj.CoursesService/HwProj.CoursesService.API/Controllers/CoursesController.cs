﻿using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HwProj.CoursesService.API.Extensions;
using HwProj.CoursesService.API.Models;
using HwProj.CoursesService.API.Models.ViewModels;
using HwProj.CoursesService.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace HwProj.CoursesService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : Controller
    {
        private readonly ICoursesService _coursesService;
        private readonly IMapper _mapper;

        public CoursesController(ICoursesService coursesService, IMapper mapper)
        {
            _coursesService = coursesService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<CourseViewModel[]> GetAll()
        {
            return _mapper.Map<CourseViewModel[]>(await _coursesService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var course = await _coursesService.GetAsync(id);
            return course == null
                ? NotFound()
                : Ok(_mapper.Map<CourseViewModel>(course)) as IActionResult;
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse([FromBody] CreateCourseViewModel courseViewModel)
        {
            var mentorId = Request.GetUserId();
            var course = _mapper.Map<Course>(courseViewModel);
            return Ok(await _coursesService.AddAsync(course, mentorId));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(long id)
        {
            var course = await _coursesService.GetAsync(id);
            if (course == null)
            {
                return Ok();
            }

            var userId = Request.GetUserId();
            if (userId != course.MentorId)
            {
                return Forbid();
            }
            
            await _coursesService.DeleteAsync(id);
            return Ok();
        }

        [HttpPost("update/{courseId}")]
        public async Task<IActionResult> UpdateCourse(long courseId, [FromBody] UpdateCourseViewModel courseViewModel)
        {
            var course = await _coursesService.GetAsync(courseId);
            if (course == null)
            {
                return Ok();
            }

            var userId = Request.GetUserId();
            if (userId != course.MentorId)
            {
                return Forbid();
            }
            
            await _coursesService.UpdateAsync(courseId, new Course()
            {
                Name = courseViewModel.Name,
                GroupName = courseViewModel.GroupName,
                IsComplete = courseViewModel.IsComplete,
                IsOpen = courseViewModel.IsOpen
            });

            return Ok();
        }

        [HttpPost("sign_in_course/{courseId}")]
        public async Task<IActionResult> SignInCourse(long courseId)
        {
            var studentId = Request.GetUserId();
            return await _coursesService.AddStudentAsync(courseId, studentId)
                ? Ok()
                : NotFound() as IActionResult;
        }

        [HttpPost("accept_student/{courseId}")]
        public async Task<IActionResult> AcceptStudent(long courseId, [FromQuery] string studentId)
        {
            var course = await _coursesService.GetAsync(courseId);
            if (course == null)
            {
                return Ok();
            }

            var userId = Request.GetUserId();
            if (userId != course.MentorId)
            {
                return Forbid();
            }
            
            return await _coursesService.AcceptCourseMateAsync(courseId, studentId)
                ? Ok()
                : NotFound() as IActionResult;
        }


        [HttpPost("reject_student/{courseId}")]
        public async Task<IActionResult> RejectStudent(long courseId, [FromQuery] string studentId)
        {
            var course = await _coursesService.GetAsync(courseId);
            if (course == null)
            {
                return Ok();
            }

            var userId = Request.GetUserId();
            if (userId != course.MentorId)
            {
                return Forbid();
            }
            
            return await _coursesService.RejectCourseMateAsync(courseId, studentId)
                ? Ok()
                : NotFound() as IActionResult;
        }

        [HttpGet("student_courses/{studentId}")]
        public IActionResult GetStudentCourses(string studentId)
        {
            var userId = Request.GetUserId();
            return userId == studentId
                ? Ok(_coursesService.GetStudentCourses(studentId))
                : Forbid() as IActionResult;
        }

        [HttpGet("mentor_courses/{mentorId}")]
        public IActionResult GetMentorCourses(string mentorId)
        {
            var userId = Request.GetUserId();
            return userId == mentorId
                ? Ok(_coursesService.GetMentorCourses(mentorId))
                : Forbid() as IActionResult;
        }
    }
}

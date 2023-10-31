﻿namespace StudentApi_H3_Database.Models
{
    public class StudentCourse
    {
        public int StudentId {  get; set; } 
        public int CourseId {  get; set; } 
        public int Character {  get; set; } 

        public Student Student { get; set; } = null!;
        public Course Course { get; set; } = null!;


    }
}

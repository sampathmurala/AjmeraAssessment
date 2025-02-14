﻿using AjmeraAssessment.Models;
using System.Collections.Generic;

namespace AjmeraAssessment.Interfaces
{
    public interface IAuthorRepository
    {

        List<Author> GetAll();
        Author GetById(int id);
        Author GetByName(string name);
        void Insert(Author author);
        void Update(Author author);
        void Delete(Author author);
    }
}

﻿using PawFect.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFect.Business.Abstract
{
    public interface ICommentService
    {
        Comment GeyById(int id);
        void Create(Comment entity);
        void Update(Comment entity);
        void Delete(Comment entity);
    }
}

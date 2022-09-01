﻿using EntityFrameworkCore.Triggered;
using Microsoft.EntityFrameworkCore;
using SUMBER.Data;
using SUMBER.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SUMBER.Infrastructure
{
    public class SoftDeleteTrigger : IBeforeSaveTrigger<ISoftDelete>
    {
        readonly ApplicationDbContext _dataContext;
        public SoftDeleteTrigger(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task BeforeSave(ITriggerContext<ISoftDelete> context, CancellationToken cancellationToken)
        {
            if (context.ChangeType == ChangeType.Deleted)
            {
                var entry = _dataContext.Entry(context.Entity);
                context.Entity.TarHapus = DateTime.UtcNow;
                context.Entity.FlHapus = 1;
                entry.State = EntityState.Modified;
            }
            await Task.CompletedTask;
        }
    }
}

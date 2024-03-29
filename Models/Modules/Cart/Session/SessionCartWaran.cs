﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SUMBER.Infrastructure;
using System;

namespace SUMBER.Models.Modules.Cart.Session
{
    public class SessionCartWaran : CartWaran
    {
        public static CartWaran GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;
            SessionCartWaran cart = session?.GetJson<SessionCartWaran>("CartWaran") ??
                new SessionCartWaran();
            cart.Session = session;
            return cart;
        }
        private ISession Session { get; set; }

        //Waran1
        public override void AddItem1(
            int abWaranId,
            decimal amaun,
            int akCartaId,
            int? jBahagianId,
            string tk
           )
        {
            base.AddItem1(abWaranId,
                          amaun,
                          akCartaId,
                          jBahagianId,
                          tk);

            Session.SetJson("CartWaran", this);
        }

        public override void RemoveItem1(int id, int id2)
        {
            base.RemoveItem1(id, id2);
            Session.SetJson("CartWaran", this);
        }
        public override void Clear1()
        {
            base.Clear1();
            Session.Remove("CartWaran");
        }
        //Waran1 End
    }
}

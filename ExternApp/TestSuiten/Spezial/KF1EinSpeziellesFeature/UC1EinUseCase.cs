﻿using ExternApp.Level1.Level2a;
using System;

namespace ExternApp.TestSuiten.Spezial.KF1EinSpeziellesFeature
{
    public class UC1EinUseCase
    {

        public void TC1()
        {
            Console.WriteLine(this.GetType().FullName + " - " + Helpers.ExecutedMethod);
        }

        public void TC2()
        {
            Console.WriteLine(this.GetType().FullName + " - " + Helpers.ExecutedMethod);
        }

        public void TC3()
        {
            Console.WriteLine(this.GetType().FullName + " - " + Helpers.ExecutedMethod);
        }
    }
}

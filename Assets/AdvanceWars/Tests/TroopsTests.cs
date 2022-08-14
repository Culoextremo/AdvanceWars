﻿using AdvanceWars.Runtime;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;

namespace AdvanceWars.Tests
{
    public class TroopsTests
    {
        [Test]
        public void aUnit_IsFriendly_WhetherSameMotherland()
        {
            using var _ = new AssertionScope();

            new Unit { Motherland = new Nation("Friend") }
                .IsFriend(new Unit { Motherland = new Nation("Friend") })
                .Should().BeTrue();

            new Unit { Motherland = new Nation("Friend") }
                .IsFriend(new Unit { Motherland = new Nation("Enemy") })
                .Should().BeFalse();
        }

        //"Coalición" es un conjunto de naciones aliadas, para cuando haya 2vs2.
        //Apátrida: una unidad sin nación. 
        //2 apatridas no deberian compartir nacion
    }
}
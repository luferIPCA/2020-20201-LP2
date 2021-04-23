﻿/*
*	<copyright file="Collections.cs" company="IPCA">
*		Copyright (c) 2021 All Rights Reserved
*	</copyright>
* 	<author>lufer</author>
*   <date>4/16/2021 11:10:44 AM</date>
*	<description></description>
**/
using System;

namespace MyCollections
{
    /// <summary>
    /// Purpose:
    /// Created by: lufer
    /// Created on: 4/16/2021 11:10:44 AM
    /// </summary>
    /// <remarks></remarks>
    /// <example></example>
    public class Pessoa
    {
        #region Attributes
        //ATENÇÃO: "public" apenas para demonstração
        public int idade;
        public string nome;
        #endregion

        #region Methods

        #region Constructors

        /// <summary>
        /// The default Constructor.
        /// </summary>
        public Pessoa()
        {
        }
        public Pessoa(string n, int i)
        {
            nome = n;
            idade = i;
        }

        #endregion

        #region Properties
        #endregion



        #region Overrides
        #endregion

        #region OtherMethods
        #endregion

        #region Destructor
        /// <summary>
        /// The destructor.
        /// </summary>
        ~Pessoa()
        {
        }
        #endregion

        #endregion
    }
}

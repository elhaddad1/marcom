﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>


<div class="dropdown" id="dropdown_two">
                                                <div class="jGM_box" id="Depts-jGlide">
                                                <%=
                                                new Marcom.Models.Ar_PublicMethod().CreateDeptMenu(11)%>
                                                </div>
                                            </div>
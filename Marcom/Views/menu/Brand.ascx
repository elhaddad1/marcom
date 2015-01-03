<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<div class="dropdown" id="dropdown_two">
                                                <div class="jGM_box" id="Brands-jGlide">
                                                <%=
                                                new Marcom.Models.PublicMethod().CreateBrndMenu(11)%>
                                                </div>
                                            </div>

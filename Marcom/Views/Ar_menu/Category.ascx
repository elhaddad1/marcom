<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<div class="dropdown" id="dropdown_one">
                                                <div class="jGM_box" id="jGlide_050">
                                                    <!-- Tiles for Menu -->
                                                     <%=
                                                new Marcom.Models.Ar_PublicMethod().CreateCtgoryMenu(50)%>
                                               
                                                    <!-- Tiles for Menu -->
                                                </div>
                                            </div>
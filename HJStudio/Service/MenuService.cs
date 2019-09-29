using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HJStudio.Data;
using HJStudio.Models;

namespace HJStudio.Service
{
    public class MenuService
    {
        public static JsTree3Node GetMenuTreeByUser(int? UserId)
        {
            try
            {
                using (HJStudioEntities db = new HJStudioEntities())
                {
                    int Cid = 0;
                    var user = db.EmployeeMasters.Find(UserId);
                    if (user.UserType == (int)UserType.Admin)
                        Cid = user.UserType ?? 0;
                    else
                        Cid = 0;
                    // var admin = CommonService.GetFranchiseeAdmin(Cid);
                    int? adminId = CommanModel.LoginUserDetails.EmployeeID ;
                    //if (admin != null)
                    //{
                    //    adminId = admin.UserId;
                    //}
                    var root = new JsTree3Node()
                    {
                        id = "0",
                        text = "HJ",
                        state = new JsTree3Node.State(true, false, false)
                    };
                    var children1 = new List<JsTree3Node>();
                    List<int> list = db.MenuUserAllocations.Where(x => x.UserId == UserId).Select(x => x.MenuId ?? 0).ToList();
                    var menu = db.MenuMasters.Where(x => x.MenuUserAllocations.Any(y => (y.UserId == adminId || adminId == 0)));
                    foreach (var item in menu.Where(x => x.ParentID == null).ToList())
                    {
                        var chields = menu.Where(x => x.ParentID == item.MenuID).ToList();
                        var node = (list.Contains(item.MenuID) && (chields == null || chields.Count == 0)) ? JsTree3Node.NewNode(item.MenuID, item.MenuName, true) : JsTree3Node.NewNode(item.MenuID, item.MenuName, false);
                        foreach (var itemChild in chields)
                        {
                            bool ChildSel = list.Contains(itemChild.MenuID);
                            node.children.Add(JsTree3Node.NewNode(itemChild.MenuID, itemChild.MenuName, ChildSel));
                        }
                        node.state.opened = false;
                        children1.Add(node);
                    }
                    root.children = children1;
                    return root;
                    // return Json(root, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<MenuModel> GetAllUserMenuDataBYRole()
        {
            try
            {
                using (HJStudioEntities context = new HJStudioEntities())
                {
                    //int cid = CommonModel.LoginUserDetails.FranchiseeId;
                    //var admin = CommonService.GetFranchiseeAdmin(cid);
                    //List<int> list = CommonService.GetFranchiseeAndChildFranchisee();
                    return context.EmployeeMasters.Where(x =>  x.Status == 1).Select(x => new MenuModel
                    {
                        UserId = x.EmployeeID,
                        UserName = x.Name
                    }).OrderByDescending(x => x.UserId).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<MenuModel> GetMenuList(string Search, int startIndex, int endIndex, int sortColumnIndex, string sortDirection, out int Total)
        {
            try
            {
                using (HJStudioEntities context = new HJStudioEntities())
                {
                    Total = 0;

                    //int cid = CommonModel.LoginUserDetails.FranchiseeId;
                    //var admin = CommonService.GetFranchiseeAdmin(cid);
                    //List<int> Flist = CommonService.GetFranchiseeAndChildFranchisee();
                    var list = context.EmployeeMasters.Where(x =>  x.Status == 1).Select(x => new MenuModel
                    {
                        UserId = x.EmployeeID,
                        UserName = x.Name
                    }).ToList();


                    if (sortColumnIndex > 0)
                    {
                        if (sortDirection == "desc")
                            switch (sortColumnIndex)
                            {
                                case 1:
                                    list = list.OrderByDescending(x => x.UserName).ToList();
                                    break;
                                case 2:
                                    list = list.OrderByDescending(x => x.FranchiseeName).ToList();
                                    break;

                            }

                        if (sortDirection == "asc")
                            switch (sortColumnIndex)
                            {
                                case 1:
                                    list = list.OrderBy(x => x.UserName).ToList();
                                    break;
                                case 2:
                                    list = list.OrderBy(x => x.FranchiseeName).ToList();
                                    break;

                            }
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.UserId).ToList();
                    }

                    if (!string.IsNullOrEmpty(Search))
                    {
                        Search = Search.ToLower();

                        list = list.Where(x =>
                        (x.UserName != null ? x.UserName.ToLower().Contains(Search) : true) ||
                        (x.FranchiseeName != null ? x.FranchiseeName.ToLower().Contains(Search) : true)
                        ).ToList();
                    }
                    Total = list != null ? list.Count() : 0;
                    list = list.Skip(startIndex).Take(endIndex).ToList();

                    return list;

                }
            }
            catch (Exception)
            {

                Total = 0;
                return null;
            }
        }


        public static int AddEditUserMenu(MenuModel _model)
        {
            try
            {
                using (HJStudioEntities context = new HJStudioEntities())
                {
                    context.MenuUserAllocations.Where(x => x.UserId == _model.UserId).ToList().ForEach(x =>
                    {
                        context.MenuUserAllocations.Remove(x);
                    });
                    foreach (var Mid in _model.MenuIdList)
                    {
                        if (Mid > 0)
                        {
                            MenuUserAllocation _MenuUserAllocationTable = new MenuUserAllocation();
                            _MenuUserAllocationTable.UserId = _model.UserId;
                            _MenuUserAllocationTable.MenuId = Mid;
                            context.MenuUserAllocations.Add(_MenuUserAllocationTable);
                        }
                    }
                    context.SaveChanges();
                    return _model.UserId.Value;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<JsTree3Node> GetUserMenu(int? UserId)
        {
            try
            {
                using (HJStudioEntities db = new HJStudioEntities())
                {
                    var User = db.EmployeeMasters.Find(UserId);
                    var children1 = new List<JsTree3Node>();
                    if (User.UserType != (int)UserType.User)
                    {

                        List<int> list = db.MenuUserAllocations.Where(x => x.UserId == UserId).Select(x => x.MenuId ?? 0).ToList();

                        foreach (var item in db.MenuMasters.Where(x => x.ParentID == null).OrderBy(x => x.DisplayOrder).ToList())
                        {
                            var chields = db.MenuMasters.Where(x => x.ParentID == item.MenuID && list.Contains(x.MenuID)).OrderBy(x => x.DisplayOrder).ToList();
                            if (chields != null && chields.Count() > 0)
                            {
                                var node = JsTree3Node.NewNode(item.MenuID, item.MenuName, false, item.MenuLink, item.MenuIcon);
                                foreach (var itemChild in chields)
                                {
                                    node.children.Add(JsTree3Node.NewNode(itemChild.MenuID, itemChild.MenuName, false, itemChild.MenuLink, itemChild.MenuIcon));
                                }
                                node.state.opened = false;
                                children1.Add(node);
                            }
                            else if (list.Contains(item.MenuID))
                            {
                                var node = JsTree3Node.NewNode(item.MenuID, item.MenuName, true, item.MenuLink, item.MenuIcon);
                                node.state.opened = false;
                                children1.Add(node);
                            }

                        }
                    }
                    else
                    {

                        foreach (var item in db.MenuMasters.Where(x => x.ParentID == null).OrderBy(x => x.DisplayOrder).ToList())
                        {
                            var chields = db.MenuMasters.Where(x => x.ParentID == item.MenuID && x.IsForEmployee == true).OrderBy(x => x.DisplayOrder).ToList();
                            if (chields != null && chields.Count() > 0)
                            {
                                var node = JsTree3Node.NewNode(item.MenuID, item.MenuName, false, item.MenuLink, item.MenuIcon);
                                foreach (var itemChild in chields)
                                {
                                    node.children.Add(JsTree3Node.NewNode(itemChild.MenuID, itemChild.MenuName, false, itemChild.MenuLink, itemChild.MenuIcon));
                                }
                                node.state.opened = false;
                                children1.Add(node);
                            }
                            else if (item.IsForEmployee == true)
                            {
                                var node = JsTree3Node.NewNode(item.MenuID, item.MenuName, true, item.MenuLink, item.MenuIcon);
                                node.state.opened = false;
                                children1.Add(node);
                            }

                        }
                    }
                    return children1;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

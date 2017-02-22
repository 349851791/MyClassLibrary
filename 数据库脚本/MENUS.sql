prompt Importing table MENUS...
set feedback off
set define off
insert into MENUS (ID, FATHERID, NAME, ISSHOW, URL, ORDERS, ICON, SELECTED, ROWID)
values (1, 0, '对象方式管理', 1, null, 2, 'icon-lefttree-object', 'true', 'AAA1kSACHAAAACEAAA');

insert into MENUS (ID, FATHERID, NAME, ISSHOW, URL, ORDERS, ICON, SELECTED, ROWID)
values (2, 0, '字典方式管理', 1, null, 3, 'icon-lefttree-dic', null, 'AAA1kSACHAAAACEAAB');

insert into MENUS (ID, FATHERID, NAME, ISSHOW, URL, ORDERS, ICON, SELECTED, ROWID)
values (3, 1, '查询方法', 1, 'WebForms/01_ObjectHTML/ObjectSelect.html', 1, null, null, 'AAA1kSACHAAAACEAAC');

insert into MENUS (ID, FATHERID, NAME, ISSHOW, URL, ORDERS, ICON, SELECTED, ROWID)
values (4, 1, '编辑方法', 1, 'WebForms/01_ObjectHTML/ObjectEdit.html', 2, null, null, 'AAA1kSACHAAAACEAAD');

insert into MENUS (ID, FATHERID, NAME, ISSHOW, URL, ORDERS, ICON, SELECTED, ROWID)
values (5, 2, '查询方法', 1, 'WebForms/02_DicHTML/DicSelect.html', 1, null, null, 'AAA1kSACHAAAACEAAE');

insert into MENUS (ID, FATHERID, NAME, ISSHOW, URL, ORDERS, ICON, SELECTED, ROWID)
values (6, 2, '编辑方法', 1, 'WebForms/02_DicHTML/DicEdit.html', 2, null, null, 'AAA1kSACHAAAACEAAF');

insert into MENUS (ID, FATHERID, NAME, ISSHOW, URL, ORDERS, ICON, SELECTED, ROWID)
values (7, 1, '辅助查询方法', 1, 'WebForms/01_ObjectHTML/AssistSelect.html', 3, null, null, 'AAA1kSACHAAAACDAAA');

insert into MENUS (ID, FATHERID, NAME, ISSHOW, URL, ORDERS, ICON, SELECTED, ROWID)
values (8, 0, '创建实体类', 1, null, 1, 'icon-lefttree-connect', null, 'AAA1kSACHAAAACDAAB');

insert into MENUS (ID, FATHERID, NAME, ISSHOW, URL, ORDERS, ICON, SELECTED, ROWID)
values (9, 8, 'oracle数据库', 1, 'WebForms/CreateModel/ByORCL.html', 1, null, null, 'AAA1kSACHAAAACDAAC');

insert into MENUS (ID, FATHERID, NAME, ISSHOW, URL, ORDERS, ICON, SELECTED, ROWID)
values (10, 0, '前端效果Demo', 1, null, 4, 'icon-lefttree-qd', null, 'AAA1kSACHAAAACFAAA');

insert into MENUS (ID, FATHERID, NAME, ISSHOW, URL, ORDERS, ICON, SELECTED, ROWID)
values (11, 0, '功能模块Demo', 1, null, 5, 'icon-lefttree-mk', null, 'AAA1kSACHAAAACFAAB');

insert into MENUS (ID, FATHERID, NAME, ISSHOW, URL, ORDERS, ICON, SELECTED, ROWID)
values (12, 11, '获取table中的内容', 1, 'WebForms/04_ModuleDemo/GetTableContent.html', 1, null, null, 'AAA1kSACHAAAACEAAG');

insert into MENUS (ID, FATHERID, NAME, ISSHOW, URL, ORDERS, ICON, SELECTED, ROWID)
values (13, 11, '局部打印', 1, 'WebForms/04_ModuleDemo/PartPrint.html', 2, null, null, 'AAA1kSACHAAAACEAAH');

insert into MENUS (ID, FATHERID, NAME, ISSHOW, URL, ORDERS, ICON, SELECTED, ROWID)
values (14, 10, '图片上一闪而过的光', 1, 'WebForms/03_FrontDemo/图片上一闪而过的光.html', 1, null, null, 'AAA1kSACHAAAACEAAI');

insert into MENUS (ID, FATHERID, NAME, ISSHOW, URL, ORDERS, ICON, SELECTED, ROWID)
values (15, 11, '上传', 1, 'WebForms/04_ModuleDemo/上传/Upload.html', 3, null, null, 'AAA1kSACHAAAACFAAC');

insert into MENUS (ID, FATHERID, NAME, ISSHOW, URL, ORDERS, ICON, SELECTED, ROWID)
values (16, 10, '浮动在最上方的层-Demo', 1, 'WebForms/03_FrontDemo/浮动在最上方的层-Demo.html', 2, null, null, 'AAA1kSACHAAAACGAAA');

insert into MENUS (ID, FATHERID, NAME, ISSHOW, URL, ORDERS, ICON, SELECTED, ROWID)
values (17, 10, '浮动在最上方的层-实例', 1, 'WebForms/03_FrontDemo/浮动在最上方的层-实例.html', 3, null, null, 'AAA1kSACHAAAACGAAB');

insert into MENUS (ID, FATHERID, NAME, ISSHOW, URL, ORDERS, ICON, SELECTED, ROWID)
values (18, 10, 'easyUI子窗体中弹出全屏win控件', 1, 'WebForms/03_FrontDemo/easyUI子窗体中弹出全屏win控件.html', 4, null, null, 'AAA1kSACHAAAACGAAC');

insert into MENUS (ID, FATHERID, NAME, ISSHOW, URL, ORDERS, ICON, SELECTED, ROWID)
values (19, 10, '云朵效果', 1, 'WebForms/03_FrontDemo/云端上/云端上.html', 5, null, null, 'AAA1kSACHAAAACGAAD');

insert into MENUS (ID, FATHERID, NAME, ISSHOW, URL, ORDERS, ICON, SELECTED, ROWID)
values (20, 11, 'easyui多级表头和导出excel', 1, 'WebForms/04_ModuleDemo/easyui多级表头和导出excel/多级表头和excel导出.html', 4, null, null, 'AAA1kSACHAAAACHAAA');

insert into MENUS (ID, FATHERID, NAME, ISSHOW, URL, ORDERS, ICON, SELECTED, ROWID)
values (21, 11, 'NPOI读取excel数据显示', 1, 'WebForms/04_ModuleDemo/NPOI读取excel数据显示/NPOI_DEMO.html', 5, null, null, 'AAA1kSACHAAAACHAAB');

insert into MENUS (ID, FATHERID, NAME, ISSHOW, URL, ORDERS, ICON, SELECTED, ROWID)
values (22, 0, '类库方法Demo', 1, null, 6, 'icon-lefttree-lk', null, 'AAA1kSACHAAAACDAAD');

insert into MENUS (ID, FATHERID, NAME, ISSHOW, URL, ORDERS, ICON, SELECTED, ROWID)
values (23, 22, 'JSONDemo', null, 'WebForms/05_UtilsDemo/JSONDemo.html', 1, null, null, 'AAA1kSACHAAAACDAAE');

insert into MENUS (ID, FATHERID, NAME, ISSHOW, URL, ORDERS, ICON, SELECTED, ROWID)
values (24, 22, '枚举Demo', null, 'WebForms/05_UtilsDemo/EnumDemo.html', 2, null, null, 'AAA1kSACHAAAACDAAF');

insert into MENUS (ID, FATHERID, NAME, ISSHOW, URL, ORDERS, ICON, SELECTED, ROWID)
values (25, 0, 'ASP.NET Demo', 1, null, 7, 'icon-lefttree-asp', null, 'AAA1kSACHAAAACGAAE');

insert into MENUS (ID, FATHERID, NAME, ISSHOW, URL, ORDERS, ICON, SELECTED, ROWID)
values (26, 25, '获取服务器IP地址', 1, 'WebForms/06_ASPDemo/GetIP.aspx', 1, null, null, 'AAA1kSACHAAAACGAAF');

insert into MENUS (ID, FATHERID, NAME, ISSHOW, URL, ORDERS, ICON, SELECTED, ROWID)
values (27, 0, 'LinqDemo', 1, null, 8, 'icon-lefttree-linq', null, 'AAA1kSACHAAAACGAAG');

insert into MENUS (ID, FATHERID, NAME, ISSHOW, URL, ORDERS, ICON, SELECTED, ROWID)
values (28, 27, 'linq', 1, 'WebForms/LinqDemo/Demo.aspx', 1, null, null, 'AAA1kSACHAAAACGAAH');

prompt Done.

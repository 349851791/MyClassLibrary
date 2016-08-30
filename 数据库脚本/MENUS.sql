prompt Importing table MENUS...
set feedback off
set define off
insert into MENUS (ID, FATHERID, NAME, ISSHOW, URL, ORDERS, ICON, SELECTED, ROWID)
values (1, 0, '对象方式管理', 1, null, 2, 'icon-lefttree-object', 'true', 'AAA1kSACHAAAACEAAA');

insert into MENUS (ID, FATHERID, NAME, ISSHOW, URL, ORDERS, ICON, SELECTED, ROWID)
values (2, 0, '字典方式管理', 1, null, 3, 'icon-lefttree-dic', null, 'AAA1kSACHAAAACEAAB');

insert into MENUS (ID, FATHERID, NAME, ISSHOW, URL, ORDERS, ICON, SELECTED, ROWID)
values (3, 1, '查询方法', 1, 'WebForms/ObjectHTML/ObjectSelect.html', 1, null, null, 'AAA1kSACHAAAACEAAC');

insert into MENUS (ID, FATHERID, NAME, ISSHOW, URL, ORDERS, ICON, SELECTED, ROWID)
values (4, 1, '编辑方法', 1, 'WebForms/ObjectHTML/ObjectEdit.html', 2, null, null, 'AAA1kSACHAAAACEAAD');

insert into MENUS (ID, FATHERID, NAME, ISSHOW, URL, ORDERS, ICON, SELECTED, ROWID)
values (5, 2, '查询方法', 1, 'WebForms/DicHTML/DicSelect.html', 1, null, null, 'AAA1kSACHAAAACEAAE');

insert into MENUS (ID, FATHERID, NAME, ISSHOW, URL, ORDERS, ICON, SELECTED, ROWID)
values (6, 2, '编辑方法', 1, 'WebForms/DicHTML/DicEdit.html', 2, null, null, 'AAA1kSACHAAAACEAAF');

insert into MENUS (ID, FATHERID, NAME, ISSHOW, URL, ORDERS, ICON, SELECTED, ROWID)
values (7, 1, '辅助查询方法', 1, 'WebForms/ObjectHTML/AssistSelect.html', 3, null, null, 'AAA1kSACHAAAACDAAA');

insert into MENUS (ID, FATHERID, NAME, ISSHOW, URL, ORDERS, ICON, SELECTED, ROWID)
values (8, 0, '创建实体类', 1, null, 1, 'icon-lefttree-connect', null, 'AAA1kSACHAAAACDAAB');

insert into MENUS (ID, FATHERID, NAME, ISSHOW, URL, ORDERS, ICON, SELECTED, ROWID)
values (9, 8, 'oracle数据库', 1, 'WebForms/CreateModel/ByORCL.html', 1, null, null, 'AAA1kSACHAAAACDAAC');

insert into MENUS (ID, FATHERID, NAME, ISSHOW, URL, ORDERS, ICON, SELECTED, ROWID)
values (10, 0, '前端效果Demo', 1, null, 4, 'icon-lefttree-qd', null, 'AAA1kSACHAAAACFAAA');

insert into MENUS (ID, FATHERID, NAME, ISSHOW, URL, ORDERS, ICON, SELECTED, ROWID)
values (11, 0, '功能模块Demo', 1, null, 5, 'icon-lefttree-mk', null, 'AAA1kSACHAAAACFAAB');

insert into MENUS (ID, FATHERID, NAME, ISSHOW, URL, ORDERS, ICON, SELECTED, ROWID)
values (12, 11, '获取table中的内容', 1, 'WebForms/ModuleDemo/GetTableContent.html', 1, null, null, 'AAA1kSACHAAAACEAAG');

insert into MENUS (ID, FATHERID, NAME, ISSHOW, URL, ORDERS, ICON, SELECTED, ROWID)
values (13, 11, '局部打印', 1, 'WebForms/ModuleDemo/PartPrint.html', 5, null, null, null);

prompt Done.

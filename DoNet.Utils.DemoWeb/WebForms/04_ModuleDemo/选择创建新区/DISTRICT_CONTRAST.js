
var url;
//初始化编辑窗体
$(function () {
    $("#win_edit").window({
        title: '增加新区',
        width: 800,
        height: 600,
        href: 'EDIT.HTML'
    });
});



//设置增加功能url,并打开窗口
function Insert_open() {
    url = 'DISTRICT_CONTRASTHandler.ashx?action=Insert';
    var row = $('#dg').datagrid('getSelected');
    $("#win_edit").window({
        title: '增加新区',
        onLoad: function () {
            TboAnimate();
            $('#tt_old').tree({
                url: 'DISTRICT_CONTRASTHandler.ashx?action=GetAllDistrict',
                animate: true,
                checkbox: true,
                onlyLeafCheck: true,
                onCheck: function (node) {
                    GetNewTree(node);
                }
            });
            $('#tt_new').tree({
                dnd: true,
                onClick: function (node) {
                    RemoveNode(node);
                }
            });
        }
    });
    $('#win_edit').dialog('open').window('center');
}


//设置编辑功能url,并打开窗口
function Update_open() {
    var row = $('#dg').treegrid('getSelected');

    var isHave = false;
    var rowRoot = $('#dg').treegrid('getRoots');
    for (var i=0 ; i < rowRoot.length; i++) {
        if (row.NAME == rowRoot[i].NAME) {
            isHave = true;
        }
    }
    if (!isHave)
    {
        $.messager.alert('提示', '请选择根数据!', 'info');
        return;
    }
    if (row) {
        url = 'DISTRICT_CONTRASTHandler.ashx?action=Update';

        $("#win_edit").window({
            title: '修改新区',
            onLoad: function () {
                TboAnimate();
                $("#div_old").fadeIn(500);
                $("#div_new").fadeIn(1000);
                $("#btnSub").fadeIn(1000);
                $('#NAME').textbox('setText', row.NAME);
                $('#tt_old').tree({
                    url: 'DISTRICT_CONTRASTHandler.ashx?action=GetAllDistrict',
                    checkbox: true,
                    animate: true,
                    onlyLeafCheck: true,
                    onCheck: function (node) {
                        GetNewTree(node);
                    },
                    onLoadSuccess: function () {
                        $.post('DISTRICT_CONTRASTHandler.ashx?action=GetDistrictById', { id: row.ID }, function (result) {
                            var myJson = eval('(' + result + ')');
                            $('#tt_new').tree({
                                data: myJson,
                                dnd: true,
                                onClick: function (node) {
                                    RemoveNode(node);
                                },
                                onLoadSuccess: function () {
                                    var node = $('#tt_new').tree('getRoot');
                                    if (node) {
                                        $('#tt_new').tree('update', {
                                            target: node.target,
                                            text: row.NAME
                                        });
                                    }
                                }
                            });

                            for (var i = 0; i < myJson[0].children.length; i++) {
                                var mynode = $('#tt_old').tree('find', myJson[0].children[i].OLDid);
                                $('#tt_old').tree('check', mynode.target);
                                var rootNode1 = $("#tt_old").tree("getParent", mynode.target); //获取当前节点的父节点的父节点
                                $("#tt_old").tree("expand", rootNode1.target);
                            }
                        });
                    }
                });
            }
        });

        $('#win_edit').dialog('open').window('center');
    }
    else {
        $.messager.alert('提示', '请选择数据!', 'info');
    }
}


//当点击左侧树时,添加右侧目录树
function GetNewTree(node) {
    var nodes = $('#tt_old').tree('getChecked');
    var s = '';
    for (var i = 0; i < nodes.length; i++) {
        if (s != '') {
            s += ',';
        }
        s += "{id: '" + nodes[i].id + "',text: '" + nodes[i].text + "',iconCls :'icon-delete'}";
    }
    var str = "[{text: '" + $('#NAME').val() + "',state: 'open',children: [" + s + "]}]";
    var myJson = eval('(' + str + ')');
    $('#tt_new').tree({
        data: myJson
    });
    $("#tt_new,#btnSub").fadeIn(500);
}

function RemoveNode(node) {
    if (node.text != $('#NAME').val()) {
        $('#tt_new').tree('remove', node.target);
        var mynode = $('#tt_old').tree('find', node.id);
        $('#tt_old').tree('uncheck', mynode.target);
        if ($('#tt_new').tree('getChildren').length <= 1) {
            $('#tt_new,#btnSub').fadeOut(500);
        }
    }

}



//提交按钮
function SubmitData() {
    //左侧树有选中的情况下,才可进行操作
    if ($('#tt_old').tree('getChecked').length > 0) {
        $.messager.progress();
        var childrenName = "";
        var childrenId = "";
        var myTree = $('#tt_new').tree('getChildren')[0];
        //当右侧树有多级存在时,一般多为拖动排序出现的问题,在此给出提示
        if (myTree.children.length < $('#tt_old').tree('getChecked').length) {
            $.messager.alert('提示', '新区中不可包含多级,请重新排序!');
            $.messager.progress('close');
            return;
        }
        //拼接子集名称
        for (var i = 0; i < myTree.children.length; i++) {
            childrenName += "," + myTree.children[i].text;
            childrenId += "," + myTree.children[i].id;
        }
        //如果是修改,则把父级ID传入
        var ID = '';
        var row = $('#dg').datagrid('getSelected');
        if (row != null) {
            ID = row.ID;
        }
        $('#myForm').form('submit', {
            url: url,
            onSubmit: function (param) {
                param.childrenName = childrenName,
                param.childrenId = childrenId,
                param.ID = ID;
            },
            success: function (result) {
                if (result == '1') {
                    $.messager.alert('提示', '提交成功!');
                    $('#dg').treegrid('reload');
                    win_close();
                } else {
                    $.messager.alert('提示', '失败,请重新提交!');
                }
            }
        });
        $.messager.progress('close');
    }

}


//删除
function Delete() {
    var row = $('#dg').datagrid("getSelected");
    if (row == null) {
        $.messager.alert('提示', '请选择数据!', 'info');
    }
    else {
        var row = $('#dg').treegrid('getSelected'); 
        var isRoot = false;
        var rowRoot = $('#dg').treegrid('getRoots');
        for (var i = 0 ; i < rowRoot.length; i++) {
            if (row.NAME == rowRoot[i].NAME) {
                isRoot = true;
            }
        }
        if (isRoot) {
            $.messager.confirm('提示', '数据为根目录,该操作将删除此数据及其子数据,您确认想要删除吗?', function (r) {
                if (r) {
                    $.post('DISTRICT_CONTRASTHandler.ashx?action=DeleteRoot', { ID: row.ID }, function (result) {
                        if (result == '1') {
                            $('#dg').treegrid('reload');
                        } else {
                            $.messager.alert('错误', '失败,请重新提交!', 'error');
                        }
                    });
                }
            });
        }
        else {
            $.messager.confirm('提示', '您确认想要删除记录吗?', function (r) {
                if (r) {
                    $.post('DISTRICT_CONTRASTHandler.ashx?action=Delete', { ID: row.ID }, function (result) {
                        if (result == '1') {
                            $('#dg').treegrid('reload');
                        } else {
                            $.messager.alert('错误', '失败,请重新提交!', 'error');
                        }
                    });
                }
            });
        } 
    };
}


//文本框输入事件 
function TboAnimate() {

    $('#NAME').textbox({
        label: '新区名称:',
        labelPosition: 'left',
        prompt: '请输入新区的名称...',
        labelAlign: 'right'
    });
    $('#NAME').textbox('textbox').bind('keyup', function (e) {
        if ($('#NAME').textbox('getText').length > 0) {
            $("#div_old").fadeIn(500);
            $("#div_new").fadeIn(1000, function () {
                var nodes = $('#tt_new').tree('getRoots');
                if (nodes.length > 0) {
                    $("#btnSub").fadeIn(500);
                }
            });

            var node = $('#tt_new').tree('getRoot');
            if (node) {
                $('#tt_new').tree('update', {
                    target: node.target,
                    text: $('#NAME').textbox('getText')
                });
            }
        }
        else {
            $("#div_old").fadeOut(1000);
            $("#div_new").fadeOut(500, function () {
                $("#btnSub").fadeOut(500);
            });
        }
    });
}

function win_close() {
    $('#win_edit').dialog('close');
}






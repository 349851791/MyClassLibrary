var all = { "ID": "0", "TEXT": "全部", "VALUE": "全部", "selected": true };//给下拉框添加全部选项
//初始化编辑窗体
$(function () {
    var url = 'DISTRICTHandler.ashx?action=GetShi';
    GetDataByAJAX(url, function (result) {
        if (result.length > 1) {
            result.splice(0, 0, all);
        }
        $('#sName').combobox('loadData', result);
        if (result.length <= 1) {
            $('#sName').combobox('select', result[0].TEXT);
        }
    });
});

function SelectBysName() {
    if ($('#sName').combobox('getText') == '全部') {
        document.getElementById('div_qName').style.display = 'none';
        document.getElementById('div_jName').style.display = 'none';
        $('#div_qName').hide();
        $('#div_jName').hide();
        $('#qName').combobox('setValue', '');
        $('#jName').combobox('setValue', '');
    }
    else {
        $('#div_qName').css("display", "inline");
        var sName = $('#sName').combobox('getValue');
        var url = 'DISTRICTHandler.ashx?action=GetQu&sName=' + escape(sName);
        GetDataByAJAX(url, function (data) {
            if (data.length > 0) {
                data.splice(0, 0, all);
                $('#qName').combobox('loadData', data);
            }
        });
    }
}

function SelectByqName() {
    if ($('#qName').combobox('getText') == '全部' || $('#qName').combobox('getText') == '') {
        $('#div_jName').hide();
        $('#jName').combobox('setValue', '');
    }
    else {
        $('#div_jName').css("display", "inline");
        var qName = $('#qName').combobox('getValue');
        var sName = $('#sName').combobox('getValue');
        var url = 'DISTRICTHandler.ashx?action=GetJie&qName=' + escape(qName) + '&sName=' + escape(sName);
        GetDataByAJAX(url, function (result) {
            if (result.length > 0) { 
                result.splice(0, 0, all);
                $('#jName').combobox('loadData', result);
            }
        });
    }
}

function GetDataByAJAX(url, onSuccess) {
    $.ajax({
        type: "POST",
        url: url,
        dataType: 'json',
        async: false,
        error: function () {//请求失败处理函数
            $.messager.alert('错误', '请求失败!', 'error');
        },
        success: function (result) {
            onSuccess(result);
        }
    });
}

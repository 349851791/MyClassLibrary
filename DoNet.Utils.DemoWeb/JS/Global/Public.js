
//-----------------------------------公共方法---------------------------------------
//获取url中的参数的值
function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return (r[2]); return null;
}

//调用演示:var curr_time = new Date(); 
//         $("#JBSJ").datebox("setValue", myformatter(curr_time));
function myformatter(date) {
    var y = date.getFullYear();
    var m = date.getMonth() + 1;
    var d = date.getDate();
    return y + '-' + (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d);
}

//删除左右两端的空格
function trim(str) { 
    return str.replace(/(^\s*)|(\s*$)/g, "");
}

//-----------------------------------easyUI部分-------------------------------------
//表格中格式化时间列
//调用演示:<th width="192px" data-options="field: 'JBSJ',halign: 'center', align: 'center', formatter:Formatter_date">经办日期</th>
function Formatter_date(value, row, index) {
    var dateStr;
    if (value != null && value.indexOf(' ') >= 0) {
        dateStr = value.substr(0, value.indexOf(' '));
    }
    else {
        dateStr = value;
    }
    return dateStr;
} 
//调用演示:<input id="JBSJ" name="JBSJ" class="easyui-datebox" data-options="formatter:myformatter,parser:myparser" editable="false">
function myparser(s) {
    if (!s) return new Date();
    var ss = (s.split('-'));
    var y = parseInt(ss[0], 10);
    var m = parseInt(ss[1], 10);
    var d = parseInt(ss[2], 10);
    if (!isNaN(y) && !isNaN(m) && !isNaN(d)) {
        return new Date(y, m - 1, d);
    } else {
        return new Date();
    }
}

//鼠标在列表中悬停显示详细信息
function formatCellTooltip(value) {
    return "<span title='" + value + "'>" + value + "</span>";
}
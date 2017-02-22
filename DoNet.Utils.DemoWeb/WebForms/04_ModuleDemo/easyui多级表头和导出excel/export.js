function ChangeToTable(printDatagrid) {
    var tableString = '<table cellspacing="0" class="pb">';
    var frozenColumns = printDatagrid.datagrid("options").frozenColumns;  // �õ�frozenColumns����
    var columns = printDatagrid.datagrid("options").columns;    // �õ�columns����
    var nameList = new Array();

    // ����title
    if (typeof columns != 'undefined' && columns != '') {
        $(columns).each(function (index) {
            tableString += '\n<tr>';
            if (typeof frozenColumns != 'undefined' && typeof frozenColumns[index] != 'undefined') {
                for (var i = 0; i < frozenColumns[index].length; ++i) {
                    if (!frozenColumns[index][i].hidden) {
                        tableString += '\n<th width="' + frozenColumns[index][i].width + '"';
                        if (typeof frozenColumns[index][i].rowspan != 'undefined' && frozenColumns[index][i].rowspan > 1) {
                            tableString += ' rowspan="' + frozenColumns[index][i].rowspan + '"';
                        }
                        if (typeof frozenColumns[index][i].colspan != 'undefined' && frozenColumns[index][i].colspan > 1) {
                            tableString += ' colspan="' + frozenColumns[index][i].colspan + '"';
                        }
                        if (typeof frozenColumns[index][i].field != 'undefined' && frozenColumns[index][i].field != '') {
                            nameList.push(frozenColumns[index][i]);
                        }
                        tableString += '>' + frozenColumns[0][i].title + '</th>';
                    }
                }
            }
            for (var i = 0; i < columns[index].length; ++i) {
                if (!columns[index][i].hidden) {
                    tableString += '\n<th width="' + columns[index][i].width + '"';
                    if (typeof columns[index][i].rowspan != 'undefined' && columns[index][i].rowspan > 1) {
                        tableString += ' rowspan="' + columns[index][i].rowspan + '"';
                    }
                    if (typeof columns[index][i].colspan != 'undefined' && columns[index][i].colspan > 1) {
                        tableString += ' colspan="' + columns[index][i].colspan + '"';
                    }
                    if (typeof columns[index][i].field != 'undefined' && columns[index][i].field != '') {
                        nameList.push(columns[index][i]);
                    }
                    if (columns[index][i].title == undefined) {
                        columns[index][i].title = ' ';
                    }
                    tableString += '>' + columns[index][i].title + '</th>';
                }
            }
            tableString += '\n</tr>';
        });
    }
    //�����е�����,�˴�Ϊд��״̬,�����Ҫ�޸�
    var newList = new Array();
    newList.push(nameList.slice(0,1), nameList.slice(1,2),
                 nameList.slice(2,3), nameList.slice(8,9), nameList.slice(9,10),
                 nameList.slice(3,4), nameList.slice(10,11), nameList.slice(11,12),
                 nameList.slice(4,5), nameList.slice(12,13), nameList.slice(13,14), nameList.slice(14,15),
                 nameList.slice(5, 6), nameList.slice(6, 7), nameList.slice(7, 8));

    // ��������
    var rows = printDatagrid.datagrid("getRows"); // ��δ����ǻ�ȡ��ǰҳ��������
    for (var i = 0; i < rows.length; ++i) {
        tableString += '\n<tr>';
        for (var j = 0; j < newList.length; ++j) {

            tableString += '\n<td';
            if (newList[j].align != 'undefined' && newList[j].align != '') {
                tableString += ' style="text-align: center;"';
            }
            tableString += '>';

            tableString += rows[i][newList[j][0].field];
            tableString += '</td>';
        }
        tableString += '\n</tr>';
    }
    tableString += '\n</table>';
    return tableString;
}

function Export(strXlsName, exportGrid) {
    var f = $('<form action="export.aspx" method="post" id="fm1"></form>');
    var i = $('<input type="hidden" id="txtContent" name="txtContent" />');
    var l = $('<input type="hidden" id="txtName" name="txtName" />');
    i.val(ChangeToTable(exportGrid));
    i.appendTo(f);
    l.val(strXlsName);
    l.appendTo(f);
    f.appendTo(document.body).submit();
    //document.body.removeChild(f);
    document.body.removeChild($('#fm1')[0]);//���
}
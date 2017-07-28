function Travel(treeID) {//参数为树的ID，注意不要添加#
    var roots = $('#' + treeID).tree('getRoots'), children, i, j;
    for (i = 0; i < roots.length; i++) {
        alert(roots[i].text);
        children = $('#' + treeID).tree('getChildren', roots[i].target);
        for (j = 0; j < children.length; j++) alert(children[j].text);
    }
}



$(function () {
    $('#tt2').tree({
        checkbox: true,
        url: 'tree_data.json',
        onClick: function (node) {
            $(this).tree('toggle', node.target);
            //alert('you dbclick '+node.text);
        },
        onContextMenu: function (e, node) {
            e.preventDefault();
            $('#tt2').tree('select', node.target);
            $('#mm').menu('show', {
                left: e.pageX,
                top: e.pageY
            });
        }
    });
});
function reload() {
    var node = $('#tt2').tree('getSelected');
    if (node) {
        $('#tt2').tree('reload', node.target);
    }
    else {
        $('#tt2').tree('reload');
    }
}

function getChildren() {
    var node = $('#tt2').tree('getSelected');
    if (node) {
        var children = $('#tt2').tree('getChildren', node.target);
    }
    else {
        var children = $('#tt2').tree('getChildren');
    }
    var s = '';
    for (var i = 0; i < children.length; i++) {
        s += children[i].text + ',';
    }
    alert(s);
}

function getChecked() {
    var nodes = $('#tt2').tree('getChecked');
    var s = '';
    for (var i = 0; i < nodes.length; i++) {
        if (s != '')
            s += ',';
        s += nodes[i].text;
    }
    alert(s);
}

function getSelected() {
    var node = $('#tt2').tree('getSelected');
    alert(node.text);
}

function collapse() {
    var node = $('#tt2').tree('getSelected');
    $('#tt2').tree('collapse', node.target);
}

function expand() {
    var node = $('#tt2').tree('getSelected');
    $('#tt2').tree('expand', node.target);
}

function collapseAll() {
    var node = $('#tt2').tree('getSelected');
    if (node) {
        $('#tt2').tree('collapseAll', node.target);
    }
    else {
        $('#tt2').tree('collapseAll');
    }
}

function expandAll() {
    var node = $('#tt2').tree('getSelected');
    if (node) {
        $('#tt2').tree('expandAll', node.target);
    }
    else {
        $('#tt2').tree('expandAll');
    }
}

function append() {
    var node = $('#tt2').tree('getSelected');
    $('#tt2').tree('append', {
        parent: (node ? node.target : null),
        data: [{
            text: 'new1',
            checked: true
        }, {
            text: 'new2',
            state: 'closed',
            children: [{
                text: 'subnew1'
            }, {
                text: 'subnew2'
            }]
        }]
    });
}

function remove() {
    var node = $('#tt2').tree('getSelected');
    $('#tt2').tree('remove', node.target);
}

function update() {
    var node = $('#tt2').tree('getSelected');
    if (node) {
        node.text = '<span style="font-weight:bold">new text<\/span>';
        node.iconCls = 'icon-save';
        $('#tt2').tree('update', node);
    }
}

function isLeaf() {
    var node = $('#tt2').tree('getSelected');
    var b = $('#tt2').tree('isLeaf', node.target);
    alert(b)
}

function GetNode(type) {
    var node = $('#tt2').tree('getChecked');
    var chilenodes = '';
    var parantsnodes = '';
    var prevNode = '';
    for (var i = 0; i < node.length; i++) {

        if ($('#tt2').tree('isLeaf', node[i].target)) {
            chilenodes += node[i].text + ',';

            var pnode = $('#tt2').tree('getParent', node[i].target);
            if (prevNode != pnode.text) {
                parantsnodes += pnode.text + ',';
                prevNode = pnode.text;
            }
        }
    }
    chilenodes = chilenodes.substring(0, chilenodes.length - 1);
    parantsnodes = parantsnodes.substring(0, parantsnodes.length - 1);

    if (type == 'child') {
        return chilenodes;
    }
    else {
        return parantsnodes
    };
};
function getNodes() {
    alert(GetNode('fnode') + "," + GetNode('child'));
}

function doNode() {
    var c = "";
    var p = "";
    $(".tree-checkbox1").parent().children('.tree-title').each(function () {
        c += $(this).parent().attr('node-id') + ",";
    });
    $(".tree-checkbox2").parent().children('.tree-title').each(function () {
        p += $(this).parent().attr('node-id') + ",";
    });
    var str = (c + p);
    str = str.substring(0, str.length - 1);
    alert(str);
}

<%@ Control Language="C#" ClassName="UcEditor" %>
<link rel="stylesheet" href="../Editor/themes/default/default.css" />
<link rel="stylesheet" href="../Editor/plugins/code/prettify.css" />
<script charset="utf-8" src="../Editor/kindeditor-min.js" type="text/javascript"></script>
<script charset="utf-8" src="../Editor/lang/zh_CN.js" type="text/javascript"></script>
<script charset="utf-8" src="../Editor/plugins/code/prettify.js" type="text/javascript"></script>
<script type="text/javascript">
    var editor;
    KindEditor.ready(function (K) {
        editor = K.create('textarea[name="Body"]', {
            cssPath: '../Editor/plugins/code/prettify.css',
            uploadJson: "../Editor/asp.net/upload_json.ashx",
            fileManagerJson: "../Editor/asp.net/file_manager_json.ashx",
            allowFileManager: false,
            afterCreate: function () {
                var self = this;
                K.ctrl(document, 13, function () {
                    self.sync();
                    K('form[name="EditForm"]')[0].submit();
                });
                K.ctrl(self.edit.doc, 13, function () {
                    self.sync();
                    K('form[name="EditForm"]')[0].submit();
                });
            }
        });
        prettyPrint();
    });
</script>

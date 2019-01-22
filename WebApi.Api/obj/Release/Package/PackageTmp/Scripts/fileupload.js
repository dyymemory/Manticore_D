var Fileupload = {
	Init: function () {
			$("#btn_uploadimg").click(function () {
				var fileObj = document.getElementById("FileUpload").files[0]; // js 获取文件对象
				if (typeof (fileObj) == "undefined" || fileObj.size <= 0) {
					alert("请选择图片");
					return;
				}
				var formFile = new FormData();
				formFile.append("action", "UploadVMKImagePath");
				formFile.append("file1", fileObj); //加入文件对象
				formFile.append("file2", fileObj);
				var data = formFile;
				$.ajax({
					url: "/ManticoreApi/UploadSource/UploadFormdata",
					data: data,
					type: "Post",
					dataType: "json",
					cache: false,//上传文件无需缓存
					processData: false,
					contentType: false,
					success: function (result) {
						alert(result.Success);
					},
				})
			})
		}
}
$(function () {
	Fileupload.Init();
});
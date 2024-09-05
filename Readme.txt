开放接口：

1.加载图片
	scrollPicturebox1.LoadImage(@"C:\Users\Flying\Desktop\rock.png");
2.用委托给label添加事件
	scrollPicturebox1.SetLabelClickHandler(scrollPicturebox1.label1, Label1_Click);
3.设置线段颜色、字体
	scrollPicturebox1.SetLineColor(Color.Red);
	scrollPicturebox1.SetTextColor(Color.Green);
	scrollPicturebox1.SetTextFont(new Font("微软雅黑", 12, FontStyle.Bold));
	scrollPicturebox1.SetLineWidth(2);
4.获取起始点
	Point start =scrollPicturebox1.GetStart();
	Point end = scrollPicturebox1.GetEnd();
	
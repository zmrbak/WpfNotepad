#pragma once
namespace ZmrbakFileDialog
{
	public ref class FileSaveDialog
	{
	public:
		bool ShowDialog(int hWnd);
		DWORD selectedItemIndex;
		LPWSTR fileName;
	};
	public ref class FileOpenDialog
	{
	public:
		bool ShowDialog(int hWnd);
		DWORD selectedItemIndex;
		LPWSTR fileName;
	};
};


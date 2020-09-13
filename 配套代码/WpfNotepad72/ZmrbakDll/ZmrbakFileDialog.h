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

	public ref class PageSetupDialog
	{
	public:
		bool ShowDialog(int hWnd);

		DWORD MarginTop;
		DWORD MarginLeft;
		DWORD MarginRight;
		DWORD MarginBottom;
		
		DWORD Landscape;

		DWORD DefaultSource;

		DWORD PaperSize;

		LPWSTR PaperHeader;
		LPWSTR PaperFooter;
	};
};


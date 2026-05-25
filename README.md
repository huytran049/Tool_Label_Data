# AI Training Tool

AI Training Tool là ứng dụng Windows Forms dùng để chuẩn bị dữ liệu, gán nhãn bounding box theo định dạng YOLO, cắt ROI từ ảnh/video/camera, chạy detect thử và khởi động quá trình train model YOLO.

> Repo này chỉ nên lưu source code. Runtime DLL, model weights, dataset ảnh, file train output và các file build đã được bỏ qua trong `.gitignore` để repo nhẹ hơn 25 MB.

## Chức Năng Chính

- Tạo/mở job cấu hình train bằng file `job.config`.
- Import ảnh từ file, video hoặc camera.
- Vẽ bounding box thủ công trên ảnh và lưu label theo định dạng YOLO: `class x_center y_center width height`.
- Quản lý danh sách ảnh train trong `train.txt`.
- Cắt ROI, resize ảnh về kích thước train, capture frame từ video/camera.
- Auto label bằng model YOLO đang có.
- Chạy detect trên ảnh/video để kiểm tra model.
- Khởi động lệnh train thông qua file `train.cmd`.
- Chọn output weight mới để dùng lại cho detect.

## Cấu Trúc Project

```text
YoloDetection.sln          Solution Visual Studio
2.TrainingTool/            Ứng dụng chính: label, import data, train, detect
DetectTool/                Tool detect đơn giản để test model trên ảnh
MRZSharp/                  Wrapper YOLO CPU/GPU và cấu trúc bbox
CommonLib/                 Hàm tiện ích dùng chung
SMCLib/                    Thư viện VB.NET phụ trợ
.gitignore                 Bỏ qua runtime, build output, dataset và model lớn
```

## Yêu Cầu Môi Trường

- Windows.
- Visual Studio 2019 hoặc mới hơn.
- .NET Framework 4.8 cho `TrainingTool`, `CommonLib`, `MRZSharp`.
- .NET Framework 4.7.2 cho `DetectTool`.
- NuGet package restore.
- Runtime native DLL của OpenCV/Darknet/CUDA nếu dùng detect/train bằng GPU.

Một số DLL/model lớn không được commit lên GitHub. Cần đặt lại chúng từ release artifact hoặc thư mục runtime riêng trước khi chạy.

## Các File Không Đưa Lên GitHub

Không commit các thư mục/file sau:

- `1.Runtime/`, `0.Runtime/`, `Runtime/`
- `packages/`
- `bin/`, `obj/`, `.vs/`
- `*.dll`, `*.exe`, `*.pdb`
- `*.weights`, `*.onnx`, `*.pt`
- `*.bmp`, dataset ảnh lớn, video, archive `*.rar`, `*.zip`, `*.7z`
- `Data/`, `models/`, `runs/`, `wandb/`, `Output/`, `Logs/`, `Temp/`

Nếu cần chia sẻ runtime/model, nên đưa vào GitHub Releases, Google Drive, OneDrive hoặc Git LFS thay vì commit trực tiếp vào repo.

## Khởi Tạo Sau Khi Clone

1. Clone repo.

```powershell
git clone <repo-url>
cd <repo-folder>
```

2. Restore NuGet package trong Visual Studio hoặc bằng NuGet CLI.

```powershell
nuget restore YoloDetection.sln
```

3. Chuẩn bị runtime ngoài repo.

Cần có các DLL/model native phù hợp với máy đang chạy, vì source hiện tại có reference tới runtime local:

```text
0.Runtime/
1.Runtime/
dark.dll
OpenCvSharp*.dll
OpenCvSharpExtern.dll
DirectShowLib-2005.dll
SystemFrameWork.dll
CUDA/cuDNN DLL nếu dùng GPU
```

4. Chuẩn bị file job và dữ liệu train.

Ứng dụng mặc định tìm job ở:

```text
Data/job.config
```

Vì `Data/` không commit lên GitHub, hãy tạo lại file config và các thư mục dữ liệu cần thiết trên máy local.

Ví dụ cấu trúc runtime/data:

```text
Data/
  job.config
  train.txt
  ImageSets/
  Model/
    model.cfg
    model.weights
  Output/
model/
  running/
    model.cfg
    model.weights
train.cmd
```

Ví dụ nội dung `job.config`:

```xml
<?xml version="1.0" encoding="utf-8"?>
<SYSTEM>
  <BaseDir>Data</BaseDir>
  <CameraID></CameraID>
  <TrainWidth>1920</TrainWidth>
  <TrainHeight>1080</TrainHeight>
  <DetectWidth>1920</DetectWidth>
  <DetectHeight>1080</DetectHeight>
  <StartROIX>0</StartROIX>
  <StartROIY>0</StartROIY>
  <NumClass>2</NumClass>
  <ClassName0>OK</ClassName0>
  <ClassName1>NG</ClassName1>
  <TrainList>train.txt</TrainList>
  <Model>Model/model.cfg</Model>
  <Weight>Model/model.weights</Weight>
</SYSTEM>
```

5. Mở solution và build.

Mở `YoloDetection.sln` bằng Visual Studio, chọn platform phù hợp, thường là `x64`, sau đó build project `3.TrainingTool`.

## Cách Sử Dụng TrainingTool

### 1. Mở job

- Bấm `Open Job`.
- Chọn file `job.config`.
- Kiểm tra `Working Directory`, kích thước train/detect và danh sách class.

### 2. Thêm ảnh train

- Mở tab `Extract Data`.
- Dùng `Open Image` để mở ảnh.
- Dùng `Start Preview Camera` để xem camera, sau đó `Capture image` nếu cần lấy ảnh từ camera.
- Dùng `Open Video`, `Play Video`, `+`, `-` để lấy frame từ video.
- Dùng `Add to train` để lưu ảnh vào thư mục train và thêm vào `train.txt`.

### 3. Cắt ROI và resize

- Giữ `Shift` và kéo chuột để vẽ ROI trên ảnh lớn.
- Giữ `Alt` và kéo để di chuyển ROI.
- Chọn `Resize to fit` hoặc `Crop ROI`.
- Dùng `Crop Image`, `Add All`, `Add All Auto` nếu cần xử lý nhiều ảnh.

### 4. Gán nhãn bounding box

- Mở tab `Gắn Nhãn`.
- Chọn class cần label.
- Giữ `Ctrl` và kéo chuột trên ảnh để vẽ bounding box.
- Bấm `Save Bounding Box` để lưu label.
- Label được lưu cùng tên với ảnh, khác extension thành `.txt`.

Định dạng label:

```text
class_id x_center y_center width height
```

Tất cả tọa độ được normalize theo kích thước ảnh, dùng được với YOLO.

### 5. Auto label

- Cần có model detect đã cấu hình trong `job.config`.
- Mở ảnh cần gán nhãn.
- Bấm `Auto Draw Bound`.
- Kiểm tra lại bounding box, sửa/xóa nếu cần.
- Bấm `Save Bounding Box`.

### 6. Detect kiểm tra model

- Mở tab `Detection`.
- Bấm `Open` để chọn ảnh.
- Bấm `Detect`.
- Kết quả detect hiển thị trên ảnh và bảng result.

### 7. Train model

- Đảm bảo `train.txt`, ảnh train, label `.txt`, `model.cfg`, `model.weights` và `train.cmd` đã sẵn sàng trong working directory.
- Mở tab `Training`.
- Bấm `Start Training` để chạy `train.cmd`.
- File output weight sẽ xuất hiện trong `Data/Output/`.
- Chọn weight mong muốn và bấm `Use this model` để copy sang model đang chạy.

## Cách Commit Lên GitHub

Sau khi đã có `.gitignore`, chạy:

```powershell
git init
git add .
git status --short
```

Kiểm tra `git status` không có các file lớn như `1.Runtime`, `0.Runtime`, `packages`, `*.dll`, `*.weights`, `*.onnx`, `*.pt`, `*.bmp`, `*.rar`.

Nếu danh sách file hợp lý:

```powershell
git commit -m "Initial commit"
git branch -M main
git remote add origin <repo-url>
git push -u origin main
```

## Lưu Ý Hiện Tại

- Project đang có một số `HintPath` trỏ tới runtime local, vì vậy clone repo mới có thể cần đặt lại DLL đúng vị trí hoặc chỉnh lại reference.
- `packages/` không commit, cần restore NuGet trước khi build.
- `Data/`, model weights và dataset không commit, cần tạo/tải lại riêng cho mỗi máy.
- Nên upload file runtime/model mẫu lên GitHub Releases và ghi link tải trong README nếu muốn người khác chạy nhanh.

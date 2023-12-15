# Google Sheet Loader

## 1. Tính năng

- Câu hỏi là game thì thường có nhiều hoặc rất item, item lại có nhiều thông số như ID, Name, Icon, Sprite, Value..., thường thì sẽ được lưu trong 1 thằng ScriptableObject nào đó.
  Vậy mỗi lần muốn chỉnh sửa thông số của 1 item, hoặc muốn xoá/thêm 1 số lượng item mới, công việc này có thể khó khăn và tốn thời gian khi số lượng item hiện tại trong game là quá nhiều.

- Load data từ google sheet vào game giúp GD làm việc dễ dàng hơn trong việc chỉnh sửa thông số của các item trên sheet, và dev cũng nhàn hơn rất nhiều khi chỉ việc fetch data về game mỗi khi có yêu cầu thay đổi.

- Package Require ([Có thể tải ở đây](https://github.com/OneHit-Tech/Utility-Package)):
  - UniTask
  - Odin Inspector
  - Newtonsoft.Json (có thể tải tạm Nice Vibration Haptic)

## 2. Sử dụng

`Ý tưởng: mình có thể đọc được dữ liệu vào game dưới dạng json, vậy thì cần convert mấy cái sheet này về json và deploy lên 1 web url để sử dụng.`

### a. Thiết kế google sheet data ([Sheet mẫu tham khảo](https://docs.google.com/spreadsheets/d/1IIIjo5gRPSlIulEG8UKpblI-SoAqIkBTIZhx3srzBhw/edit#gid=0))

- Tên sheet: **Item**, **Buff**, ...
- Tên các cột: **ID**, **Name**, **Quantity**, **Type**, ...
- Kiểu dữ liệu các cột: **int**, **float**, **string**, ... _(cột Type thì có thể sử dụng int xong vào game thì convert sang enum sau)_

_Lưu ý các điểm trên vì sẽ cần sử dụng chính xác các tên sheet, tên cột, và kiểu dữ liệu để load data vào game sau khi fetch từ sheet._

![image](https://github.com/OneHit-Tech/Google-Sheet-Loader/assets/80816285/a4d8b154-4558-4c24-a12e-0d998c7f9bf0)

### b. Deploy data json lên web

- Vào **Tiện ích mở rộng** -> **Apps Script**

- Paste [Code này](https://paste.ofcode.org/Y39mAvzxWHf7tNMmYscKpZ#) (code lấy dữ liệu từ sheet chuyển thành json), save và run

- Bấm **Triển khai** -> **Tuỳ chọn triển khai mới** -> **Chọn loại** -> **Ứng dụng web** -> Setup như sau

![image](https://github.com/OneHit-Tech/Google-Sheet-Loader/assets/80816285/33da224a-a4e4-40a1-8c7d-839de6d56a9a)

- Sau khi deploy xong thì được 1 url dạng _script.googleusercontent.com/..._ với dữ liệu json như sau:

![image](https://github.com/OneHit-Tech/Google-Sheet-Loader/assets/80816285/397b68f9-8df1-4d0f-b6e5-30af449f79f9)

- Để có thể xem được json rõ hơn thì có thể cài extension [jsonv](https://chromewebstore.google.com/detail/jsonv/cgffjielkgfdhoiloknkfcimejepaodg)

![json web](https://github.com/OneHit-Tech/Google-Sheet-Loader/assets/80816285/9754cc57-0f4c-4cd9-9033-9515dba100c7)

- **Lưu ý:**
  - _Trong quá trình run / deploy yêu cầu một số quyền, cứ cấp quyền thôi (riêng deploy thì phải owner của sheet mới deploy được)_
  - _Khi update dữ liệu trong sheet, chỉ cần mở Apps Script lên và run lại để nó cập nhật lại dữ liệu json trên web_
  - _Khi muốn thay đổi cấu trúc json (sửa code Apps Script) thì cần phải deploy lại_

### c. Fetch data vào game (Quan trọng)

`Đến đoạn này rồi thì phải custom code tuỳ theo từng project rồi.`

#### B1: Khai báo url các json data đã deploy

- **Create** -> **Sheet** -> **SheetManagerSO** để tạo 1 thằng ScriptableObject như dưới đây, nó sẽ xử lý việc fetch dữ liệu từ url về game
  - `Game Data SO`: dùng để lưu các dữ liệu trong game (sẽ nói rõ hơn ở phía dưới)
  - [Original Url](https://docs.google.com/spreadsheets/d/1IIIjo5gRPSlIulEG8UKpblI-SoAqIkBTIZhx3srzBhw/edit#gid=0): link sheet gốc để tiện cho việc sửa dữ liệu
  - [Deploy Url](https://script.googleusercontent.com/macros/echo?user_content_key=VXJaJtth22RmyBWaaM2BkGLsJLE__d9hNeLWXOx5pMzNDpP-Y9HW-lX-ghx49mxHJJyCAlFZEeebmxGDBMIJVG8O9cPpxriMm5_BxDlH2jW0nuo2oDemN9CCS2h10ox_1xSncGQajx_ryfhECjZEnB4ZSY5UMFThaltBwob-t2efv9Z11mZXGRLhmwYTWlYw9rY35sCir2a3ExyUqWwnIMYrmb6zKZhoUPn7LeOGbtGqSoYSfFQBmNz9Jw9Md8uu&lib=MRSUt4NTG0V3STyuKSFFDImlGxTIUvSrR): link json đã deploy lên web, dùng để xử lý

![sheetso](https://github.com/OneHit-Tech/Google-Sheet-Loader/assets/80816285/20b73a6f-c583-4acc-bf86-9c1cace7cd83)

#### B2: Định nghĩa các class cho _data trên sheet_ và _data trong game_

- Đầu tiên cần định nghĩa các class có các trường thuộc tính ứng với dữ liệu trên json, để có thể **DeserializeObject**. Như trên sheet, ta có các class sau:
  - `ItemInSheet` là dữ liệu 1 hàng trong sheet `Item`
  - `BuffInSheet` là dữ liệu 1 hàng trong sheet `Buff`
  - `SheetData` đại diện cho cả 1 spreadsheet gồm danh sách dữ liệu các hàng trong 2 sheet `Item` và `Buff`

```cs
// Tên trường phải trùng với tên cột trong sheet
// Thứ tự các trường phải trùng với thứ tự các cột trong sheet

[Serializable]
public class ItemInSheet
{
    public int ID;
    public string Name;
    public int Quantity;
    public int Type; // 1: Attack, 2: Defense
}

[Serializable]
public class BuffInSheet
{
    public int ID;
    public string Name;
    public float Value;
    public int Quantity;
}

[Serializable]
public class SheetData
{
    public List<ItemInSheet> Item;
    public List<BuffInSheet> Buff;
}
```

- Tiếp theo là định nghĩa các class chứa data trong game. Trong game thì data có thể có nhiều thuộc tính hơn là trên sheet, rồi từ cái game data này mình mới đi xử lý các logic trong game mình.
- Tiện thì mình xử lý luôn phần convert từ DataInSheet => Data ngay trong hàm khởi tạo luôn.

```cs
public enum EItemType
{
    Attack = 1,
    Defense = 2,
}

[Serializable]
public class ItemData
{
    public int id;
    public string name;
    public int quantity;
    public EItemType type;

    public ItemData(ItemInSheet itemInSheet)
    {
        id = itemInSheet.ID;
        name = itemInSheet.Name;
        quantity = itemInSheet.Quantity;
        type = (EItemType)itemInSheet.Type;
    }
}

[Serializable]
public class BuffData
{
    public int id;
    public string name;
    public float value;
    public int quantity;

    public BuffData(BuffInSheet buffInSheet)
    {
        id = buffInSheet.ID;
        name = buffInSheet.Name;
        value = buffInSheet.Value;
        quantity = buffInSheet.Quantity;
    }
}
```

> _Có thể bạn sẽ thấy tại sao lại làm phức tạp thế, thì đơn giản là như trong VD này, trong game mình cần thuộc tính Type của Item là 1 cái enum, nhưng trên sheet thì nó chỉ có thể là int thôi, nên vẫn cần 1 class đại diện cho dữ liệu trên sheet, và 1 class đại diện cho dữ liệu trong game để convert qua lại cho chuẩn logic._

#### B3. Load dữ liệu vào game

- Bạn có thể tìm thấy hàm `FetchData()` trong class `SheetDataSO`, sau khi dữ liệu được fetch về, việc đơn giản là load nó vào game.

  - Như trong VD, mình sử dụng 1 thằng ScriptableObject `GameDataSO` để lưu tất cả data trong game.
  - Và do các url của data json này được khai báo đầu `sheetUrls`, nên dữ liệu của nó sau khi convert vào game là `sheetDataList[0]`

```cs
 public class SheetDataSO : ScriptableObject
{
    public GameDataSO gameDataSO;
    public List<SheetUrl> sheetUrls = new();

    private async void FetchData()
    {
        var sheetDataList = new List<SheetData>();
        // handle fetch data ...
        Debug.Log("Fetch data complete!".Color("lime"));

        // * EXAMPLE: load data to game
        var dataGameInSheet = sheetDataList[0];
        gameDataSO.LoadDataFromSheet(dataGameInSheet);
    }
}

public class GameDataSO : ScriptableObject
{
    public List<ItemData> itemDataSet = new();
    public List<BuffData> buffDataSet = new();

    public void LoadDataFromSheet(SheetData sheetData)
    {
        Debug.Log("Load data into game".Color("yellow"));

        itemDataSet.Clear();
        sheetData.Item.ForEach(itemInSheet =>
            itemDataSet.Add(new ItemData(itemInSheet))
        );

        buffDataSet.Clear();
        sheetData.Buff.ForEach(buffInSheet =>
            buffDataSet.Add(new BuffData(buffInSheet))
        );
    }
}
```

- Nếu có nhiều link sheet data, cứ tạo thêm phần tử trong `sheetUrls` của `SheetDataSO`, và nhớ code thêm xử lý phần load dữ liệu vào game.

#### B4: Demo (Thử với sample để hiểu rõ)

![sheetso](https://github.com/OneHit-Tech/Google-Sheet-Loader/assets/80816285/f6a6d731-da97-41bf-9281-282ff9c715b7)

![sheetso](https://github.com/OneHit-Tech/Google-Sheet-Loader/assets/80816285/7bf6f0f5-ac7d-40e9-bf44-0b7bd49c3657)

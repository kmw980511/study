#include "Default.h";

//void TestFunc(int& Param) //참조에 의한 호출
//{
//	Param = 100;
//}

//void Swap(int& a, int& b) //참조전달
//{
//	int tmp = a;
//	a = b;
//	b = tmp;
//}

int main(void)
{
#pragma region cout 사용법
	//cout << 10 << endl;
	//cout << 10.5 << endl;
	//cout << 10U << endl;
	//cout << 10.1235 << endl;
	//cout << 10.5f << endl;
	//cout << 5 + 7 << endl;
#pragma endregion

#pragma region 문자열 조합
	//string strData = "Test Data";
	//cout << "Sample string" << endl;
	//cout << strData << endl;

	//strData = "New String"; //strData에 값을 새로 변경함

	//cout << strData << endl; 

	//cout << "저는" << 20 << "살" << "입니다" << endl;
#pragma endregion

#pragma region cin 사용법
	//int Age;
	//cout << "나이를 입력하세요" << endl;
	//cin >> Age;

	//char Job[32];
	//cout << "직업을 적으세요" << endl;
	//cin >> Job;

	//string Name;
	//cout << "이름을 적으세요" << endl;
	//cin >> Name;

	//cout << "당신의 이름은 " << Name << "이고 나이는 " << Age << "이고 직업은 " << Job << "입니다" << endl;

#pragma endregion

#pragma region auto 예약어 사용
	//int a = 10;
	//int b(a);
	//auto c(b);

	//cout << a + b + c << endl;
#pragma endregion

#pragma region new 연산자 이용
	//int* Data = new int; //인스턴스만 동적으로 생성하는 경우
	//int* NewData = new int(10);//초기값을 사용하는 경우

	//*Data = 5;
	//cout << *Data << endl;
	//cout << *NewData << endl;

	//delete Data;
	//delete NewData;
#pragma endregion

#pragma region 배열형태의 객체 생성
	//int* arr = new int[5]; //객체를 배열형태로 동적생성
	//for (int i = 0; i < 5; ++i)
	//	arr[i] = (i + 1) * 10;

	//for (int i = 0; i < 5; ++i)
	//	cout << arr[i] << endl;

	//delete[]arr; //배열형태로 생성한 대상은 반드시 배열형태로 삭제해야한다.
#pragma endregion

#pragma region 참조형 변수 사용
	//int nData = 10; 
	//int& ref = nData; //Data변수에 대한 참조자 선언

	//ref = 20; //참조자의 값을 변경하면 원본도 변경이 됨
	//cout << nData << endl;

	//int *pnData = &nData;
	//*pnData = 30;
	//cout << nData << endl;

#pragma endregion

#pragma region 참조에 의한 호출
	//int Data = 0;

	//TestFunc(Data);
	//cout << Data << endl;
#pragma endregion

#pragma region 참조전달
	//int x = 10, y = 20;
	//Swap(x, y); //참조전달이 되며 둘의 값이 교환됨

	//cout << "x : " << x << endl;
	//cout << "y : " << y << endl;
#pragma endregion


}

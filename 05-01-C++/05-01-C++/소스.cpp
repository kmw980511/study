#include "Default.h";

//void TestFunc(int& Param) //������ ���� ȣ��
//{
//	Param = 100;
//}

//void Swap(int& a, int& b) //��������
//{
//	int tmp = a;
//	a = b;
//	b = tmp;
//}

int main(void)
{
#pragma region cout ����
	//cout << 10 << endl;
	//cout << 10.5 << endl;
	//cout << 10U << endl;
	//cout << 10.1235 << endl;
	//cout << 10.5f << endl;
	//cout << 5 + 7 << endl;
#pragma endregion

#pragma region ���ڿ� ����
	//string strData = "Test Data";
	//cout << "Sample string" << endl;
	//cout << strData << endl;

	//strData = "New String"; //strData�� ���� ���� ������

	//cout << strData << endl; 

	//cout << "����" << 20 << "��" << "�Դϴ�" << endl;
#pragma endregion

#pragma region cin ����
	//int Age;
	//cout << "���̸� �Է��ϼ���" << endl;
	//cin >> Age;

	//char Job[32];
	//cout << "������ ��������" << endl;
	//cin >> Job;

	//string Name;
	//cout << "�̸��� ��������" << endl;
	//cin >> Name;

	//cout << "����� �̸��� " << Name << "�̰� ���̴� " << Age << "�̰� ������ " << Job << "�Դϴ�" << endl;

#pragma endregion

#pragma region auto ����� ���
	//int a = 10;
	//int b(a);
	//auto c(b);

	//cout << a + b + c << endl;
#pragma endregion

#pragma region new ������ �̿�
	//int* Data = new int; //�ν��Ͻ��� �������� �����ϴ� ���
	//int* NewData = new int(10);//�ʱⰪ�� ����ϴ� ���

	//*Data = 5;
	//cout << *Data << endl;
	//cout << *NewData << endl;

	//delete Data;
	//delete NewData;
#pragma endregion

#pragma region �迭������ ��ü ����
	//int* arr = new int[5]; //��ü�� �迭���·� ��������
	//for (int i = 0; i < 5; ++i)
	//	arr[i] = (i + 1) * 10;

	//for (int i = 0; i < 5; ++i)
	//	cout << arr[i] << endl;

	//delete[]arr; //�迭���·� ������ ����� �ݵ�� �迭���·� �����ؾ��Ѵ�.
#pragma endregion

#pragma region ������ ���� ���
	//int nData = 10; 
	//int& ref = nData; //Data������ ���� ������ ����

	//ref = 20; //�������� ���� �����ϸ� ������ ������ ��
	//cout << nData << endl;

	//int *pnData = &nData;
	//*pnData = 30;
	//cout << nData << endl;

#pragma endregion

#pragma region ������ ���� ȣ��
	//int Data = 0;

	//TestFunc(Data);
	//cout << Data << endl;
#pragma endregion

#pragma region ��������
	//int x = 10, y = 20;
	//Swap(x, y); //���������� �Ǹ� ���� ���� ��ȯ��

	//cout << "x : " << x << endl;
	//cout << "y : " << y << endl;
#pragma endregion


}

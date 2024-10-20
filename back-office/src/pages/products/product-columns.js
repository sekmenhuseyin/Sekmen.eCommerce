import { Button, Popconfirm } from 'antd'
import { EditOutlined, DeleteOutlined } from '@ant-design/icons';

export default function productColumns(edit, remove) {
  return [
    {
      title: 'Id',
      dataIndex: 'id',
    },
    {
      title: 'Name',
      dataIndex: 'name',
    },
    {
      title: 'Price',
      dataIndex: 'price',
    },
    {
      title: 'Description',
      dataIndex: 'description',
    },
    {
      title: 'CategoryName',
      dataIndex: 'categoryName',
    },
    {
      title: 'ImageUrl',
      dataIndex: 'imageUrl',
    },
    {
      title: '',
      align: 'center',
      fixed: 'right',
      render: (_, row) => (
        <>
          <Button key="edit" type="primary" onClick={() => edit(row)}>
            <EditOutlined />
          </Button>
          <Popconfirm
            key="delete"
            title="Are you sure you want to delete this?"
            onConfirm={() => remove(row)}
            okText="Yes"
            cancelText="No"
          >
            <Button type="danger">
              <DeleteOutlined />
            </Button>
          </Popconfirm>
        </>
      )
    }
  ]
}
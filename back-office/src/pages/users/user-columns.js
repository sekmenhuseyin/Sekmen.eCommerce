import { Button, Popconfirm } from 'antd'
import { EditOutlined, DeleteOutlined } from '@ant-design/icons';

export default function userColumns(edit, remove) {
  return [
    {
      title: 'Name',
      width: 120,
      dataIndex: 'name',
    },
    {
      title: 'E-mail',
      dataIndex: 'email',
    },
    {
      title: 'Phone',
      dataIndex: 'phone',
    },
    {
      title: '',
      align: 'center',
      width: 110,
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
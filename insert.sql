-- ������
insert into grids(horz, plast, slice, field, grid_name, pts, add_pts, del_pts, 
grid, src_sql, srf_options, grid_options)
values(47500, 2, 0, 'apk','������',' ',' ',' ','0',' ',' ',' ');

-- �������
insert into grids(horz, plast, slice, field, grid_name, pts, add_pts, del_pts, 
grid, src_sql, srf_options, grid_options)
values(47500, 2, 0, 'app','�������',' ',' ',' ','0',' ',' ',' ');

-- ���� ����������
insert into grids(horz, plast, slice, field, grid_name, pts, add_pts, del_pts, 
grid, src_sql, srf_options, grid_options)
values(47500, 2, 0, 'dk','���� ����������',' ',' ',' ','0',' ',' ',' ');

-- �������������
insert into grids(horz, plast, slice, field, grid_name, pts, add_pts, del_pts, 
grid, src_sql, srf_options, grid_options)
values(47500, 2, 0, 'k','�������������',' ',' ',' ','0',' ',' ',' ');

-- ����������
insert into grids(horz, plast, slice, field, grid_name, pts, add_pts, del_pts, 
grid, src_sql, srf_options, grid_options)
values(47500, 2, 0, 'm','����������',' ',' ',' ','0',' ',' ',' ');

-- �����������������
insert into grids(horz, plast, slice, field, grid_name, pts, add_pts, del_pts, 
grid, src_sql, srf_options, grid_options)
values(47500, 13, 0, 's','�����������������',' ',' ',' ','0',' ',' ',' ');


-- ����
begin
for i in 0..10 loop
insert into grids(horz, plast, slice, field, grid_name, pts, add_pts, del_pts, 
grid, src_sql, srf_options, grid_options)
values(47500, 3, i, 'apk','������',' ',' ',' ','0',' ',' ',' ');
insert into grids(horz, plast, slice, field, grid_name, pts, add_pts, del_pts, 
grid, src_sql, srf_options, grid_options)
values(47500, 3, i, 'app','�������',' ',' ',' ','0',' ',' ',' ');
insert into grids(horz, plast, slice, field, grid_name, pts, add_pts, del_pts, 
grid, src_sql, srf_options, grid_options)
values(47500, 3, i, 'k','�������������',' ',' ',' ','0',' ',' ',' ');
insert into grids(horz, plast, slice, field, grid_name, pts, add_pts, del_pts, 
grid, src_sql, srf_options, grid_options)
values(47500, 3, i, 'm','����������',' ',' ',' ','0',' ',' ',' ');
insert into grids(horz, plast, slice, field, grid_name, pts, add_pts, del_pts, 
grid, src_sql, srf_options, grid_options)
values(47500, 3, i, 's','�����������������',' ',' ',' ','0',' ',' ',' ');
end loop;
end;
/
commit;
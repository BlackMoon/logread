-- Кровля
insert into grids(horz, plast, slice, field, grid_name, pts, add_pts, del_pts, 
grid, src_sql, srf_options, grid_options)
values(47500, 2, 0, 'apk','Кровля',' ',' ',' ','0',' ',' ',' ');

-- Подошва
insert into grids(horz, plast, slice, field, grid_name, pts, add_pts, del_pts, 
grid, src_sql, srf_options, grid_options)
values(47500, 2, 0, 'app','Подошва',' ',' ',' ','0',' ',' ',' ');

-- Доля коллектора
insert into grids(horz, plast, slice, field, grid_name, pts, add_pts, del_pts, 
grid, src_sql, srf_options, grid_options)
values(47500, 2, 0, 'dk','Доля коллектора',' ',' ',' ','0',' ',' ',' ');

-- Проницаемость
insert into grids(horz, plast, slice, field, grid_name, pts, add_pts, del_pts, 
grid, src_sql, srf_options, grid_options)
values(47500, 2, 0, 'k','Проницаемость',' ',' ',' ','0',' ',' ',' ');

-- Пористость
insert into grids(horz, plast, slice, field, grid_name, pts, add_pts, del_pts, 
grid, src_sql, srf_options, grid_options)
values(47500, 2, 0, 'm','Пористость',' ',' ',' ','0',' ',' ',' ');

-- Нефтенасыщенность
insert into grids(horz, plast, slice, field, grid_name, pts, add_pts, del_pts, 
grid, src_sql, srf_options, grid_options)
values(47500, 13, 0, 's','Нефтенасыщенность',' ',' ',' ','0',' ',' ',' ');


-- цикл
begin
for i in 0..10 loop
insert into grids(horz, plast, slice, field, grid_name, pts, add_pts, del_pts, 
grid, src_sql, srf_options, grid_options)
values(47500, 3, i, 'apk','Кровля',' ',' ',' ','0',' ',' ',' ');
insert into grids(horz, plast, slice, field, grid_name, pts, add_pts, del_pts, 
grid, src_sql, srf_options, grid_options)
values(47500, 3, i, 'app','Подошва',' ',' ',' ','0',' ',' ',' ');
insert into grids(horz, plast, slice, field, grid_name, pts, add_pts, del_pts, 
grid, src_sql, srf_options, grid_options)
values(47500, 3, i, 'k','Проницаемость',' ',' ',' ','0',' ',' ',' ');
insert into grids(horz, plast, slice, field, grid_name, pts, add_pts, del_pts, 
grid, src_sql, srf_options, grid_options)
values(47500, 3, i, 'm','Пористость',' ',' ',' ','0',' ',' ',' ');
insert into grids(horz, plast, slice, field, grid_name, pts, add_pts, del_pts, 
grid, src_sql, srf_options, grid_options)
values(47500, 3, i, 's','Нефтенасыщенность',' ',' ',' ','0',' ',' ',' ');
end loop;
end;
/
commit;
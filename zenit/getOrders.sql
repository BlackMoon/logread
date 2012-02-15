-- --------------------------------------------------------------------------------
-- Routine DDL
-- Note: comments before and after the routine body will not be stored by the server
-- --------------------------------------------------------------------------------
DELIMITER $$

CREATE DEFINER=`zenit`@`%` PROCEDURE `getOrders`(surname char(50), name char(50), patronymic char(50), datefrom DateTime, dateTo DateTime)
BEGIN
    select distinct c.surname, c.name, c.patronymic, p.product, p.price, Date(o.orderdate) as orderdate, h.surname as cashiersurname, h.name as cashiername from clients c
left join orders o on o.clientid = c.id
left join products p on p.id = o.productid
left join cashiers h on h.id = o.cashierid
where Date(o.orderdate) between dateFrom And dateTo
And c.surname like Surname and c.name like name and c.patronymic like patronymic;

    
END
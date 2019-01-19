function A=MITVR2019Hack_format_data(A, name)
    A(:, 2:size(A, 2))=A(:, 2:size(A, 2))./max(max(A));
    A=A(:, [1, 8, 9, 10, 11, 12, 13, 26, 27, 28, 47, 48, 49, 32, 33, 34, 53, 54, 55, 41, 42, 43, 62, 63, 64, 74, 75, 76, 95, 96, 97, 113, 114, 115, 98, 99, 100, 116, 117, 118]);
    fid=fopen(name, 'w');
    for j=1:size(A, 2)
        fprintf(fid, '%d, ', A(1, j));
    end
    for i=2:size(A, 1)
        deltaTime=A(i, 1)-A(i-1, 1);
        fprintf(fid, '%d, ', A(i, 1));
        for j=2:size(A, 2)
            fprintf(fid, '%d, ', (A(i, j)-A(i-1, j))/deltaTime);
        end
    end
    fclose(fid);
end
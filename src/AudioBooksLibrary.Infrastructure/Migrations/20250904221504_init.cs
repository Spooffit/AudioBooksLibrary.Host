using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AudioBooksLibrary.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "audiobooks",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    authors_line = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    cover_url = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    created_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_audiobooks", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "authors",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_authors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    display_name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "audiobook_parts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    audiobook_id = table.Column<Guid>(type: "uuid", nullable: false),
                    index = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    file_path = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    md5 = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    file_size_bytes = table.Column<long>(type: "bigint", nullable: false),
                    duration_ms = table.Column<int>(type: "integer", nullable: false),
                    mime_type = table.Column<string>(type: "text", nullable: true),
                    created_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_audiobook_parts", x => x.id);
                    table.ForeignKey(
                        name: "fk_audiobook_parts_audiobooks_audiobook_id",
                        column: x => x.audiobook_id,
                        principalTable: "audiobooks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "audiobook_authors",
                columns: table => new
                {
                    audiobook_id = table.Column<Guid>(type: "uuid", nullable: false),
                    author_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_audiobook_authors", x => new { x.audiobook_id, x.author_id });
                    table.ForeignKey(
                        name: "fk_audiobook_authors_audiobooks_audiobook_id",
                        column: x => x.audiobook_id,
                        principalTable: "audiobooks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_audiobook_authors_authors_author_id",
                        column: x => x.author_id,
                        principalTable: "authors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "playback_progresses",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    audiobook_id = table.Column<Guid>(type: "uuid", nullable: false),
                    part_id = table.Column<Guid>(type: "uuid", nullable: true),
                    position_ms = table.Column<int>(type: "integer", nullable: false),
                    is_completed = table.Column<bool>(type: "boolean", nullable: false),
                    updated_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    completed_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_playback_progresses", x => new { x.user_id, x.audiobook_id });
                    table.ForeignKey(
                        name: "fk_playback_progresses_audiobook_parts_part_id",
                        column: x => x.part_id,
                        principalTable: "audiobook_parts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_playback_progresses_audiobooks_audiobook_id",
                        column: x => x.audiobook_id,
                        principalTable: "audiobooks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_playback_progresses_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "timeline_markers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    audiobook_id = table.Column<Guid>(type: "uuid", nullable: false),
                    part_id = table.Column<Guid>(type: "uuid", nullable: true),
                    position_ms = table.Column<int>(type: "integer", nullable: false),
                    text = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    created_by_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_timeline_markers", x => x.id);
                    table.ForeignKey(
                        name: "fk_timeline_markers_audiobook_parts_part_id",
                        column: x => x.part_id,
                        principalTable: "audiobook_parts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_timeline_markers_audiobooks_audiobook_id",
                        column: x => x.audiobook_id,
                        principalTable: "audiobooks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_timeline_markers_users_created_by_user_id",
                        column: x => x.created_by_user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "timeline_marker_likes",
                columns: table => new
                {
                    marker_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_timeline_marker_likes", x => new { x.marker_id, x.user_id });
                    table.ForeignKey(
                        name: "fk_timeline_marker_likes_timeline_markers_marker_id",
                        column: x => x.marker_id,
                        principalTable: "timeline_markers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_timeline_marker_likes_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_audiobook_authors_author_id",
                table: "audiobook_authors",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "ix_audiobook_parts_audiobook_id_index",
                table: "audiobook_parts",
                columns: new[] { "audiobook_id", "index" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_audiobook_parts_md5",
                table: "audiobook_parts",
                column: "md5",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_playback_progresses_audiobook_id",
                table: "playback_progresses",
                column: "audiobook_id");

            migrationBuilder.CreateIndex(
                name: "ix_playback_progresses_part_id",
                table: "playback_progresses",
                column: "part_id");

            migrationBuilder.CreateIndex(
                name: "ix_timeline_marker_likes_user_id",
                table: "timeline_marker_likes",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_timeline_markers_audiobook_id_part_id_position_ms",
                table: "timeline_markers",
                columns: new[] { "audiobook_id", "part_id", "position_ms" });

            migrationBuilder.CreateIndex(
                name: "ix_timeline_markers_created_by_user_id",
                table: "timeline_markers",
                column: "created_by_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_timeline_markers_part_id",
                table: "timeline_markers",
                column: "part_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "audiobook_authors");

            migrationBuilder.DropTable(
                name: "playback_progresses");

            migrationBuilder.DropTable(
                name: "timeline_marker_likes");

            migrationBuilder.DropTable(
                name: "authors");

            migrationBuilder.DropTable(
                name: "timeline_markers");

            migrationBuilder.DropTable(
                name: "audiobook_parts");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "audiobooks");
        }
    }
}
